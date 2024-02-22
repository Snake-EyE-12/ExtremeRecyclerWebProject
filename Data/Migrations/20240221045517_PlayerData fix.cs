using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeRecycler.Data.Migrations
{
    public partial class PlayerDatafix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "binCurrentCapacity",
                table: "PlayerData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "binMaxCapacity",
                table: "PlayerData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "binValue",
                table: "PlayerData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "binCurrentCapacity",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "binMaxCapacity",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "binValue",
                table: "PlayerData");
        }
    }
}
