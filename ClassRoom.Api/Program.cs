
using ClassRoom.Application;
using ClassRoom.Application.Services;
using ClassRoom.Domain;
using ClassRoom.Infrastracture;
using MessageBrocker.Messages;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace ClassRoom.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ClassRoomDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("ClassRoomDb")));
            builder.Services.AddScoped<IClassRoomDbContext>(provider => provider.GetRequiredService<ClassRoomDbContext>());
            builder.Services.AddScoped<ClassRoomService>(); 
            builder.Services.AddScoped<ClassRoomTypeService>();
            builder.Services.AddScoped<BuildingService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Default", policyBuilder => policyBuilder

                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });

            builder.Services.AddSingleton(_ => new ConnectionFactory()
            {
                HostName = builder.Configuration["RabbitMqHost"]
            });

            builder.Services.AddSingleton<IModel>(provider =>
            {
                var factory = provider.GetRequiredService<ConnectionFactory>();
                return factory.CreateConnection().CreateModel();
            });

            builder.Services.AddSingleton<IMessageReceiver, MessageReceiver>();

            var app = builder.Build();

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    InitializeDatabase(app);
                    Thread.Sleep(i * 1000);
                }
                catch(Exception)
                {
                    continue;
                }

                break;
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();

            app.UseCors("Default");
            app.MapControllers();

            SubscribeMessages(app);

            app.Run();
        }

        private static void SubscribeMessages(WebApplication app)
        {
            using var sc = app.Services.CreateScope();
            var receiver = sc.ServiceProvider.GetRequiredService<IMessageReceiver>();
            
            var scopeFactory = sc.ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            receiver.Subscribe<BuildingCreated>(MessageQueues.BuildingCreatedQueue, async (ev) =>
            {
                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IClassRoomDbContext>();

                var b = new Building()
                {
                    Id = ev.Id,
                    Name = ev.Name
                };

                context.Buildings.Add(b);
                await context.SaveChangesAsync();
            });

            receiver.Subscribe<BuildingUpdated>(MessageQueues.BuildingUpdatedQueue, async (ev) =>
            {
                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IClassRoomDbContext>();

                var b = context.Buildings.FirstOrDefault(x => x.Id == ev.Id);
                if (b != null)
                {
                    b.Name = ev.Name;
                    await context.SaveChangesAsync();
                }
            });

            receiver.Subscribe<BuildingRemoved>(MessageQueues.BuildingRemovedQueue, async (ev) =>
            {
                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IClassRoomDbContext>();

                var b = context.Buildings.FirstOrDefault(x => x.Id == ev.Id);
                if (b != null)
                {
                    context.Buildings.Remove(b);
                    await context.SaveChangesAsync();
                }
            });
        }

        private static void InitializeDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ClassRoomDbContext>();
            context.Database.Migrate();
        }
    }
}
