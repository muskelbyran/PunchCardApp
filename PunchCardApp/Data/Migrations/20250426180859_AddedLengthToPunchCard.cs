using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunchCardApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedLengthToPunchCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "PunchCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "PunchCards");
        }
    }
}
