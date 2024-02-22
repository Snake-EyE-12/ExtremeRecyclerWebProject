using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeRecycler.Data.Migrations
{
    public partial class PlayerUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUpgrades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssociatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpgradeID = table.Column<int>(type: "int", nullable: false),
                    UpgradeLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUpgrades", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUpgrades");
        }
    }
}
