using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MicroservicioReservaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPago", x => x.MetodoPagoId);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    NumeroAsiento = table.Column<int>(type: "int", nullable: false),
                    Clase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasajeroId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK_Pago_MetodoPago_MetodoPagoId",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodoPago",
                        principalColumn: "MetodoPagoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pago_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pasaje",
                columns: table => new
                {
                    PasajeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasaje", x => x.PasajeId);
                    table.ForeignKey(
                        name: "FK_Pasaje_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PagoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Factura_Pago_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pago",
                        principalColumn: "PagoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MetodoPago",
                columns: new[] { "MetodoPagoId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Tarjeta" },
                    { 2, "Mercado Pago" }
                });

            migrationBuilder.InsertData(
                table: "Reserva",
                columns: new[] { "ReservaId", "Clase", "Fecha", "NumeroAsiento", "PasajeroId", "Precio", "UsuarioId", "ViajeId" },
                values: new object[] { 1, "Alta", new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), 4, 0, 2000, new Guid("00000000-0000-0000-0000-000000000000"), 0 });

            migrationBuilder.InsertData(
                table: "Pago",
                columns: new[] { "PagoId", "Fecha", "MetodoPagoId", "Monto", "ReservaId" },
                values: new object[] { 1, new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), 1, 2000, 1 });

            migrationBuilder.InsertData(
                table: "Factura",
                columns: new[] { "FacturaId", "Estado", "Fecha", "Monto", "PagoId" },
                values: new object[] { 1, "Paga", new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), 2000, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_PagoId",
                table: "Factura",
                column: "PagoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pago_MetodoPagoId",
                table: "Pago",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ReservaId",
                table: "Pago",
                column: "ReservaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pasaje_ReservaId",
                table: "Pasaje",
                column: "ReservaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Pasaje");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "Reserva");
        }
    }
}
