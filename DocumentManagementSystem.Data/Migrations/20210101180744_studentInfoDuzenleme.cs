using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class studentInfoDuzenleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfo_Program_ProgramsId",
                table: "StudentInfo");

            migrationBuilder.DropIndex(
                name: "IX_StudentInfo_ProgramsId",
                table: "StudentInfo");

            migrationBuilder.DropColumn(
                name: "ProgramsId",
                table: "StudentInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramsId",
                table: "StudentInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_ProgramsId",
                table: "StudentInfo",
                column: "ProgramsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfo_Program_ProgramsId",
                table: "StudentInfo",
                column: "ProgramsId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
