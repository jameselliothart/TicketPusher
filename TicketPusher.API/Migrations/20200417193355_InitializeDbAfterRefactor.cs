using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class InitializeDbAfterRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(nullable: false),
                    owner = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    submit_date = table.Column<DateTime>(nullable: true),
                    due_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.ticket_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");
        }
    }
}
