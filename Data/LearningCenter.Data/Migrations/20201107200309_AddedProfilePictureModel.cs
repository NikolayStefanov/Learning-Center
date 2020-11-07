using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCenter.Data.Migrations
{
    public partial class AddedProfilePictureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_ProfilePicture_ProfilePictureId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePicture_AspNetUsers_UserId",
                table: "ProfilePicture");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ProfilePicture_ProfilePictureId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePicture",
                table: "ProfilePicture");

            migrationBuilder.RenameTable(
                name: "ProfilePicture",
                newName: "ProfilePictures");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePicture_UserId",
                table: "ProfilePictures",
                newName: "IX_ProfilePictures_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePicture_IsDeleted",
                table: "ProfilePictures",
                newName: "IX_ProfilePictures_IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ProfilePictures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePictures",
                table: "ProfilePictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_ProfilePictures_ProfilePictureId",
                table: "Lecturers",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePictures_AspNetUsers_UserId",
                table: "ProfilePictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ProfilePictures_ProfilePictureId",
                table: "Students",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_ProfilePictures_ProfilePictureId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePictures_AspNetUsers_UserId",
                table: "ProfilePictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ProfilePictures_ProfilePictureId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePictures",
                table: "ProfilePictures");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProfilePictures");

            migrationBuilder.RenameTable(
                name: "ProfilePictures",
                newName: "ProfilePicture");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePicture",
                newName: "IX_ProfilePicture_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePictures_IsDeleted",
                table: "ProfilePicture",
                newName: "IX_ProfilePicture_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePicture",
                table: "ProfilePicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_ProfilePicture_ProfilePictureId",
                table: "Lecturers",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePicture_AspNetUsers_UserId",
                table: "ProfilePicture",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ProfilePicture_ProfilePictureId",
                table: "Students",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
