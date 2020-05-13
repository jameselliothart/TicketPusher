using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class CreateSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "completed_ticket",
                columns: table => new
                {
                    completed_ticket_id = table.Column<Guid>(nullable: false),
                    owner = table.Column<string>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    submit_date = table.Column<DateTime>(nullable: true),
                    due_date = table.Column<DateTime>(nullable: true),
                    completion_date = table.Column<DateTime>(nullable: true),
                    resolution = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_completed_ticket", x => x.completed_ticket_id);
                    table.ForeignKey(
                        name: "FK_completed_ticket_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(nullable: false),
                    owner = table.Column<string>(nullable: false),
                    project_id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    submit_date = table.Column<DateTime>(nullable: true),
                    due_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_ticket_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_completed_ticket_project_id",
                table: "completed_ticket",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_project_id",
                table: "ticket",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "completed_ticket");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "project");
        }
    }
}
