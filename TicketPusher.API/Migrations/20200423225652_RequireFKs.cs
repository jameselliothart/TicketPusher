using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class RequireFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_tickets_projects_ProjectId",
                table: "completed_tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "tickets",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "completed_tickets",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completed_tickets_projects_ProjectId",
                table: "completed_tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "completed_tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_completed_tickets_projects_ProjectId",
                table: "completed_tickets",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_projects_ProjectId",
                table: "tickets",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
