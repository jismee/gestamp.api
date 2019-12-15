using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestamp.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    SaleChannel = table.Column<string>(nullable: true),
                    OrderPriority = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    ShipDate = table.Column<DateTime>(nullable: false),
                    UnitsSold = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    UnitCost = table.Column<double>(nullable: false),
                    TotalRevenue = table.Column<double>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    TotalProfit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    PasswordHast = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
