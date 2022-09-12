using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareCalServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "CalendarViews",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarViews", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventInclusions",
                columns: table => new
                {
                    CalendarGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    CalendarEventGuid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventInclusions", x => new { x.CalendarGuid, x.CalendarEventGuid });
                    table.ForeignKey(
                        name: "FK_CalendarEventInclusions_CalendarEvents_CalendarEventGuid",
                        column: x => x.CalendarEventGuid,
                        principalTable: "CalendarEvents",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEventInclusions_Calendars_CalendarGuid",
                        column: x => x.CalendarGuid,
                        principalTable: "Calendars",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarSources",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    CalendarSourceType = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceString = table.Column<string>(type: "TEXT", nullable: false),
                    CalendarViewGuid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarSources", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_CalendarSources_CalendarViews_CalendarViewGuid",
                        column: x => x.CalendarViewGuid,
                        principalTable: "CalendarViews",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CachedCalendarEvents",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Retrieved = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CalendarSourceGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedCalendarEvents", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_CachedCalendarEvents_CalendarSources_CalendarSourceGuid",
                        column: x => x.CalendarSourceGuid,
                        principalTable: "CalendarSources",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedCalendarEvents_CalendarSourceGuid",
                table: "CachedCalendarEvents",
                column: "CalendarSourceGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventInclusions_CalendarEventGuid",
                table: "CalendarEventInclusions",
                column: "CalendarEventGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarSources_CalendarViewGuid",
                table: "CalendarSources",
                column: "CalendarViewGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedCalendarEvents");

            migrationBuilder.DropTable(
                name: "CalendarEventInclusions");

            migrationBuilder.DropTable(
                name: "CalendarSources");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "CalendarViews");
        }
    }
}
