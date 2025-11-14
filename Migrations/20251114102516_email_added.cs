using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class email_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");
        }
    }
}
