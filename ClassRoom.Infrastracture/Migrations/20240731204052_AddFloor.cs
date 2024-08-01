using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassRoom.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddFloor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "ClassRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "ClassRooms");
        }
    }
}
