using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassRoom.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClassRoomTypeId = table.Column<short>(type: "smallint", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassRooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);

                    table.ForeignKey(
                        name: "FK_ClassRooms_ClassRoomTypes_ClassRoomTypeId",
                        column: x => x.ClassRoomTypeId,
                        principalTable: "ClassRoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClassRoomTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { (short)0, "Не указано" },
                    { (short)1, "Лекционное" },
                    { (short)2, "Для практических занятий" },
                    { (short)3, "Спортзал" },
                    { (short)4, "Прочее" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRooms_BuildingId",
                table: "ClassRooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRooms_ClassRoomTypeId",
                table: "ClassRooms",
                column: "ClassRoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassRooms");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "ClassRoomTypes");
        }
    }
}
