using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class AddParentProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_project_id",
                table: "project",
                nullable: true);

            migrationBuilder.InsertData(
                table: "project",
                columns: new[] { "project_id", "name", "parent_project_id" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "", null });

            migrationBuilder.CreateIndex(
                name: "IX_project_parent_project_id",
                table: "project",
                column: "parent_project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_project_parent_project_id",
                table: "project",
                column: "parent_project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_project_parent_project_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_parent_project_id",
                table: "project");

            migrationBuilder.DeleteData(
                table: "project",
                keyColumn: "project_id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DropColumn(
                name: "parent_project_id",
                table: "project");
        }
    }
}
