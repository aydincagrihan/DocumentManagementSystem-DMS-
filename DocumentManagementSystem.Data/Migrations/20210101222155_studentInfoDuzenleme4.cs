using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class studentInfoDuzenleme4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfo_User_UserId",
                table: "StudentInfo");

            migrationBuilder.DropIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StudentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfo_User_UserId",
                table: "StudentInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
