using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PunchCardApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedFeedbacksystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTypo = table.Column<bool>(type: "bit", nullable: false),
                    IsBug = table.Column<bool>(type: "bit", nullable: false),
                    IsIdea = table.Column<bool>(type: "bit", nullable: false),
                    IsPraise = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
        }
    }
}
