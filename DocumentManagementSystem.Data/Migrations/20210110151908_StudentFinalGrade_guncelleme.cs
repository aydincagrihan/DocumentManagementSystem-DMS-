using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class StudentFinalGrade_guncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentFinalGrade_ReportsId",
                table: "StudentFinalGrade",
                column: "ReportsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFinalGrade_Report_ReportsId",
                table: "StudentFinalGrade",
                column: "ReportsId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentFinalGrade_Report_ReportsId",
                table: "StudentFinalGrade");

            migrationBuilder.DropIndex(
                name: "IX_StudentFinalGrade_ReportsId",
                table: "StudentFinalGrade");
        }
    }
}
