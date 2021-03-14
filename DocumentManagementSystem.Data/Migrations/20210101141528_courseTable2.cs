using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class courseTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramsId",
                table: "Course",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course",
                column: "ProgramsId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramsId",
                table: "Course",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Program_ProgramsId",
                table: "Course",
                column: "ProgramsId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
