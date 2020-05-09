using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class SingularizeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_tickets_projects_project_id",
                table: "completed_tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_project_id",
                table: "tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tickets",
                table: "tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projects",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_completed_tickets",
                table: "completed_tickets");

            migrationBuilder.RenameTable(
                name: "tickets",
                newName: "ticket");

            migrationBuilder.RenameTable(
                name: "projects",
                newName: "project");

            migrationBuilder.RenameTable(
                name: "completed_tickets",
                newName: "completed_ticket");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_project_id",
                table: "ticket",
                newName: "IX_ticket_project_id");

            migrationBuilder.RenameIndex(
                name: "IX_completed_tickets_project_id",
                table: "completed_ticket",
                newName: "IX_completed_ticket_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticket",
                table: "ticket",
                column: "ticket_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project",
                table: "project",
                column: "project_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_completed_ticket",
                table: "completed_ticket",
                column: "completed_ticket_id");

            migrationBuilder.AddForeignKey(
                name: "FK_completed_ticket_project_project_id",
                table: "completed_ticket",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_project_project_id",
                table: "ticket",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_ticket_project_project_id",
                table: "completed_ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_project_project_id",
                table: "ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticket",
                table: "ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project",
                table: "project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_completed_ticket",
                table: "completed_ticket");

            migrationBuilder.RenameTable(
                name: "ticket",
                newName: "tickets");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "projects");

            migrationBuilder.RenameTable(
                name: "completed_ticket",
                newName: "completed_tickets");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_project_id",
                table: "tickets",
                newName: "IX_tickets_project_id");

            migrationBuilder.RenameIndex(
                name: "IX_completed_ticket_project_id",
                table: "completed_tickets",
                newName: "IX_completed_tickets_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tickets",
                table: "tickets",
                column: "ticket_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projects",
                table: "projects",
                column: "project_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_completed_tickets",
                table: "completed_tickets",
                column: "completed_ticket_id");

            migrationBuilder.AddForeignKey(
                name: "FK_completed_tickets_projects_project_id",
                table: "completed_tickets",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_projects_project_id",
                table: "tickets",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
