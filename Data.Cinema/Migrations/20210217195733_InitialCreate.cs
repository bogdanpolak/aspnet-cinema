using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Cinema.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movieid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.movieid);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    roomid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    rows = table.Column<int>(type: "integer", nullable: false),
                    columns = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.roomid);
                });

            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    showid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movieid = table.Column<int>(type: "integer", nullable: false),
                    roomid = table.Column<int>(type: "integer", nullable: false),
                    start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shows_pkey", x => x.showid);
                    table.ForeignKey(
                        name: "shows_movieid_fkey",
                        column: x => x.movieid,
                        principalTable: "movies",
                        principalColumn: "movieid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "shows_roomid_fkey",
                        column: x => x.roomid,
                        principalTable: "rooms",
                        principalColumn: "roomid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ticketid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('tickets_ticket_id_seq'::regclass)"),
                    showid = table.Column<int>(type: "integer", nullable: false),
                    rownum = table.Column<int>(type: "integer", nullable: false),
                    seatnum = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.ticketid);
                    table.ForeignKey(
                        name: "tickets_showid_fkey",
                        column: x => x.showid,
                        principalTable: "shows",
                        principalColumn: "showid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shows_movieid",
                table: "shows",
                column: "movieid");

            migrationBuilder.CreateIndex(
                name: "IX_shows_roomid",
                table: "shows",
                column: "roomid");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_showid",
                table: "tickets",
                column: "showid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "shows");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "rooms");
        }
    }
}
