using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class AddProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "tickets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.project_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ProjectId",
                table: "tickets",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropIndex(
                name: "IX_tickets_ProjectId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "tickets");
        }
    }
}
