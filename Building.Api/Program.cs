using Building.Application;
using Building.Application.Services;
using Building.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using IMessageSender = Building.Application.Services.IMessageSender;

namespace Building.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BuildingContext>(optionsBuilder => optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("BuildingDb")));
            builder.Services.AddScoped<IBuildingContext>(provider => provider.GetRequiredService<BuildingContext>());
            builder.Services.AddScoped<BuildingsService>();

            builder.Services.AddScoped(_ => new ConnectionFactory()
            {
                HostName = builder.Configuration["RabbitMqHost"]
            });

            builder.Services.AddScoped<IMessageSender, MessageSender>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Default", policyBuilder => policyBuilder

                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });

            var app = builder.Build();

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    InitializeDatabase(app);
                    Thread.Sleep(i * 1000);
                }
                catch  
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

            app.Run();
        }

        private static void InitializeDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BuildingContext>();
            context.Database.Migrate();
        }
    }
}
