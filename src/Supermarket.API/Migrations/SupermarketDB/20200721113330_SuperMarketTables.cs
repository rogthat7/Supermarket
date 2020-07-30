using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Supermarket.API.Migrations.SupermarketDB
{
    public partial class SuperMarketTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    PId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PName = table.Column<string>(maxLength: 50, nullable: false),
                    PBrand = table.Column<string>(nullable: true),
                    PQuantityInStock = table.Column<short>(nullable: false),
                    UnitOfMeasurement = table.Column<byte>(nullable: false),
                    PPurchasePrice = table.Column<decimal>(nullable: false),
                    PSalesPrice = table.Column<decimal>(nullable: false),
                    PSellerID = table.Column<int>(nullable: false),
                    PBarcode = table.Column<long>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.PId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 100, "Fruits and Vegetables" },
                    { 101, "Dairy" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "PId", "CategoryId", "PBarcode", "PBrand", "PName", "PPurchasePrice", "PQuantityInStock", "PSalesPrice", "PSellerID", "UnitOfMeasurement" },
                values: new object[,]
                {
                    { 100, 100, 0L, null, "Apple", 0m, (short)1, 0m, 0, (byte)1 },
                    { 101, 101, 0L, null, "Milk", 0m, (short)2, 0m, 0, (byte)5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
