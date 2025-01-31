using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunchCardApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePunchCardUseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsedBy",
                table: "PunchCardUses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedBy",
                table: "PunchCardUses");
        }
    }
}
