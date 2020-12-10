using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCenter.Data.Migrations
{
    public partial class AddedThumbnailInLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Lectures");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Lectures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Lectures",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Lectures");

            migrationBuilder.AddColumn<string>(
                name: "VideoId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
