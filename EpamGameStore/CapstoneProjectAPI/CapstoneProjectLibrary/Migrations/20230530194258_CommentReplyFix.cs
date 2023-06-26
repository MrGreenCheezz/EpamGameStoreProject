using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CommentReplyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "CommentReplies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "CommentReplies");
        }
    }
}
