using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeRecycler.Data.Migrations
{
    public partial class Upgradedisplaynameanddescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Upgrades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Upgrades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Upgrades");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Upgrades");
        }
    }
}
