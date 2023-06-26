using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                table: "Comments");
        }
    }
}
