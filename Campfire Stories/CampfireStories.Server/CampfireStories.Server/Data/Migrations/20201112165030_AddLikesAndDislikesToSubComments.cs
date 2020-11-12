using Microsoft.EntityFrameworkCore.Migrations;

namespace CampfireStories.Server.Data.Migrations
{
    public partial class AddLikesAndDislikesToSubComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "SubComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "SubComments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "SubComments");
        }
    }
}
