using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class AddCompletedTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "completed_tickets",
                columns: table => new
                {
                    completed_ticket_id = table.Column<Guid>(nullable: false),
                    owner = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    submit_date = table.Column<DateTime>(nullable: true),
                    due_date = table.Column<DateTime>(nullable: true),
                    completion_date = table.Column<DateTime>(nullable: true),
                    resolution = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_completed_tickets", x => x.completed_ticket_id);
                    table.ForeignKey(
                        name: "FK_completed_tickets_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_completed_tickets_ProjectId",
                table: "completed_tickets",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "completed_tickets");
        }
    }
}
