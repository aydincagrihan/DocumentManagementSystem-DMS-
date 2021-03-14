using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class courseTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_ProjectType_ProjectTypeId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_ProjectTypeId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTypeId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProjectTypeId",
                table: "Course",
                column: "ProjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_ProjectType_ProjectTypeId",
                table: "Course",
                column: "ProjectTypeId",
                principalTable: "ProjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
