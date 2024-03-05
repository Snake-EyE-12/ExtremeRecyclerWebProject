using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeRecycler.Data.Migrations
{
    public partial class sellTimer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "sellAvailableTime",
                table: "PlayerData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sellAvailableTime",
                table: "PlayerData");
        }
    }
}
