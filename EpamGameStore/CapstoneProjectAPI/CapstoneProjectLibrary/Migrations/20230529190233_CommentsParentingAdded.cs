using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CommentsParentingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentPostId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentPostId",
                table: "Comments");
        }
    }
}
