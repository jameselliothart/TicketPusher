using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class UpdateColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_tickets_projects_ProjectId",
                table: "completed_tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_ProjectId",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_completed_tickets_ProjectId",
                table: "completed_tickets");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "completed_tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "tickets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "completed_tickets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tickets_project_id",
                table: "tickets",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_completed_tickets_project_id",
                table: "completed_tickets",
                column: "project_id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_tickets_projects_project_id",
                table: "completed_tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_project_id",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_project_id",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_completed_tickets_project_id",
                table: "completed_tickets");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "completed_tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "completed_tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ProjectId",
                table: "tickets",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_completed_tickets_ProjectId",
                table: "completed_tickets",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_completed_tickets_projects_ProjectId",
                table: "completed_tickets",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
