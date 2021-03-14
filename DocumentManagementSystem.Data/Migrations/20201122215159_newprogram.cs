using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class newprogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Program_ProgramId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_ProgramId",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "ProgramsId",
                table: "Course",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProgramsId",
                table: "Course",
                column: "ProgramsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course",
                column: "ProgramsId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_ProgramsId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ProgramsId",
                table: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProgramId",
                table: "Course",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Program_ProgramId",
                table: "Course",
                column: "ProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
