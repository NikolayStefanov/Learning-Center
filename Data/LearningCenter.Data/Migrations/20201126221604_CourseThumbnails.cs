using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCenter.Data.Migrations
{
    public partial class CourseThumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseThumbnails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseThumbnails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseThumbnails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ThumbnailId",
                table: "Courses",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseThumbnails_CourseId",
                table: "CourseThumbnails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseThumbnails_IsDeleted",
                table: "CourseThumbnails",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseThumbnails_ThumbnailId",
                table: "Courses",
                column: "ThumbnailId",
                principalTable: "CourseThumbnails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseThumbnails_ThumbnailId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseThumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ThumbnailId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Courses");
        }
    }
}
