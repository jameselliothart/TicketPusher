using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketPusher.API.Migrations
{
    public partial class DefaultParentProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "parent_project_id",
                table: "project",
                nullable: true,
                defaultValue: new Guid("11111111-1111-1111-1111-111111111111"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.Sql(@"
                update project
                set parent_project_id = '11111111-1111-1111-1111-111111111111'
                where project_id <> '11111111-1111-1111-1111-111111111111'
                and parent_project_id is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "parent_project_id",
                table: "project",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true,
                oldDefaultValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
