using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Livit.ABC.Domain.Migrations
{
    public partial class addeeventsourcing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsSource",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    AggregateId = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    SagaId = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsSource", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsSource");
        }
    }
}
