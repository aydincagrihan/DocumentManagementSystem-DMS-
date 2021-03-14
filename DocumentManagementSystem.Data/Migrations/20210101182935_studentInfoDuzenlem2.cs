using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class studentInfoDuzenlem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfo_UserType_UserTypeId",
                table: "StudentInfo");

            migrationBuilder.DropIndex(
                name: "IX_StudentInfo_UserTypeId",
                table: "StudentInfo");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "StudentInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "StudentInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_UserTypeId",
                table: "StudentInfo",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfo_UserType_UserTypeId",
                table: "StudentInfo",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
