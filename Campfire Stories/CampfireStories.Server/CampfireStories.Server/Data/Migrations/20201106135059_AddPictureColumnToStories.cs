using Microsoft.EntityFrameworkCore.Migrations;

namespace CampfireStories.Server.Data.Migrations
{
    public partial class AddPictureColumnToStories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Stories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Stories");
        }
    }
}
