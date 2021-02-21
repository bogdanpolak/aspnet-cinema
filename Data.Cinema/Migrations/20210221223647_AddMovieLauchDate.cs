using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Cinema.Migrations
{
    public partial class AddMovieLauchDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LaunchDate",
                table: "movies",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaunchDate",
                table: "movies");
        }
    }
}
