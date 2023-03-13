using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProjectLibrary.Migrations
{
    public partial class listUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHiden",
                table: "ToDoLists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHiden",
                table: "ToDoLists");
        }
    }
}
