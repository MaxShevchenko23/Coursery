using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursery.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class courseskills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Courses");
        }
    }
}
