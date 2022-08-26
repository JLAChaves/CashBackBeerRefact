using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashBackBeer.Infra.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashBack_Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashBack_Day = table.Column<int>(type: "int", nullable: false),
                    CreateInUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinalSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateInUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartialSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateInUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartialSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartialSales_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartialSales_FinalSales_FinalSaleId",
                        column: x => x.FinalSaleId,
                        principalTable: "FinalSales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartialSales_BeerId",
                table: "PartialSales",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartialSales_FinalSaleId",
                table: "PartialSales",
                column: "FinalSaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartialSales");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "FinalSales");
        }
    }
}
