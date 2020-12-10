using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCenter.Data.Migrations
{
    public partial class LecturesMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalResources_Courses_CourseId",
                table: "AdditionalResources");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalResources_CourseId",
                table: "AdditionalResources");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "AdditionalResources");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Lectures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LectureId",
                table: "AdditionalResources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalResources_LectureId",
                table: "AdditionalResources",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalResources_Lectures_LectureId",
                table: "AdditionalResources",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalResources_Lectures_LectureId",
                table: "AdditionalResources");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalResources_LectureId",
                table: "AdditionalResources");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "AdditionalResources");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Lectures",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "AdditionalResources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalResources_CourseId",
                table: "AdditionalResources",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalResources_Courses_CourseId",
                table: "AdditionalResources",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
