using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biciclotti.Data.Migrations
{
    /// <inheritdoc />
    public partial class nuovaMIG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bicycle",
                columns: table => new
                {
                    BicycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Modello = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodiceFornitore = table.Column<int>(type: "int", nullable: false),
                    PrezzoAcquisto = table.Column<float>(type: "real", nullable: false),
                    PrezzoVendita = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicycle", x => x.BicycleId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    CodiceOrdine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataConsegna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatoOrdine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.CodiceOrdine);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    Taglia = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    BicycleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Bicycle_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycle",
                        principalColumn: "BicycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Bicycle_BicycleId1",
                        column: x => x.BicycleId1,
                        principalTable: "Bicycle",
                        principalColumn: "BicycleId");
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    OrderRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    Taglia = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    CodiceOrdine = table.Column<int>(type: "int", nullable: false),
                    OrderCodiceOrdine = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => x.OrderRowId);
                    table.ForeignKey(
                        name: "FK_OrderRows_Bicycle_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycle",
                        principalColumn: "BicycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_CodiceOrdine",
                        column: x => x.CodiceOrdine,
                        principalTable: "Orders",
                        principalColumn: "CodiceOrdine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_OrderCodiceOrdine",
                        column: x => x.OrderCodiceOrdine,
                        principalTable: "Orders",
                        principalColumn: "CodiceOrdine");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bicycle_Marca_Modello",
                table: "Bicycle",
                columns: new[] { "Marca", "Modello" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_BicycleId",
                table: "OrderRows",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_CodiceOrdine",
                table: "OrderRows",
                column: "CodiceOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_OrderCodiceOrdine",
                table: "OrderRows",
                column: "OrderCodiceOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BicycleId_Taglia",
                table: "Stocks",
                columns: new[] { "BicycleId", "Taglia" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BicycleId1",
                table: "Stocks",
                column: "BicycleId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Bicycle");
        }
    }
}
