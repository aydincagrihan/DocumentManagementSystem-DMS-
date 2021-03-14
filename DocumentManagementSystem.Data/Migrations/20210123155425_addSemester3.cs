using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class addSemester3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Semester",
            //    table: "Course");

            //migrationBuilder.AddColumn<int>(
            //    name: "SemesterId",
            //    table: "Course",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Course_SemesterId",
            //    table: "Course",
            //    column: "SemesterId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Course_Semester_SemesterId",
            //    table: "Course",
            //    column: "SemesterId",
            //    principalTable: "Semester",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Course_Semester_SemesterId",
            //    table: "Course");

            //migrationBuilder.DropIndex(
            //    name: "IX_Course_SemesterId",
            //    table: "Course");

            //migrationBuilder.DropColumn(
            //    name: "SemesterId",
            //    table: "Course");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Semester",
            //    table: "Course",
            //    type: "datetime(6)",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
