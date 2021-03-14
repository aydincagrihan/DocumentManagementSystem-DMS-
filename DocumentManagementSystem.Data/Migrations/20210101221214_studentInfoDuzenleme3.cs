using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class studentInfoDuzenleme3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo",
                column: "UserId",
                unique: true);
        }
    }
}
