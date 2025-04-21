using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunchCardApp.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBug",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IsIdea",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IsPraise",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IsTypo",
                table: "Feedbacks");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackType",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackType",
                table: "Feedbacks");

            migrationBuilder.AddColumn<bool>(
                name: "IsBug",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIdea",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPraise",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTypo",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
