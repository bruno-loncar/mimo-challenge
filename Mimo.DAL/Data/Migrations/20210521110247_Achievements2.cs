using Microsoft.EntityFrameworkCore.Migrations;

namespace Mimo.API.Data.Migrations
{
    public partial class Achievements2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletionObjectId",
                table: "Achievements",
                newName: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CourseId",
                table: "Achievements",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_CourseId",
                table: "Achievements");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Achievements",
                newName: "CompletionObjectId");
        }
    }
}
