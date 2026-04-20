using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHub.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
