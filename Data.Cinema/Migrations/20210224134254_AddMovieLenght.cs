using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Cinema.Migrations
{
    public partial class AddMovieLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IMDB",
                table: "movies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMDB",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "movies");
        }
    }
}
