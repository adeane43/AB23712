using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AB23712_SignalR.Context.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "WeatherForecasts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WeatherForecasts");
        }
    }
}
