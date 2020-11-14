using Microsoft.EntityFrameworkCore.Migrations;

namespace CampfireStories.Server.Data.Migrations
{
    public partial class AddedIsReadToBothReportTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UserReports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "StoryReports",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UserReports");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "StoryReports");
        }
    }
}
