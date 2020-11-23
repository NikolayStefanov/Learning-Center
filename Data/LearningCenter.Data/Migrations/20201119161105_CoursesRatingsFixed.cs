using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCenter.Data.Migrations
{
    public partial class CoursesRatingsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesRatings");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Courses_CourseId",
                table: "Ratings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Courses_CourseId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Ratings");

            migrationBuilder.CreateTable(
                name: "CoursesRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesRatings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursesRatings_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesRatings_CourseId",
                table: "CoursesRatings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesRatings_IsDeleted",
                table: "CoursesRatings",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesRatings_RatingId",
                table: "CoursesRatings",
                column: "RatingId");
        }
    }
}
