using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fora.Migrations
{
    /// <inheritdoc />
    public partial class createDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EdgarCompanyDataList",
                columns: table => new
                {
                    Cik = table.Column<long>(type: "bigint", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    standardFundableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    specialFundableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdgarCompanyDataList", x => x.Cik);
                });

            migrationBuilder.CreateTable(
                name: "InfoFactUsGaapIncomeLossUnitsUsd",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Val = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EdgarCompanyDataCik = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoFactUsGaapIncomeLossUnitsUsd", x => x.id);
                    table.ForeignKey(
                        name: "FK_InfoFactUsGaapIncomeLossUnitsUsd_EdgarCompanyDataList_EdgarCompanyDataCik",
                        column: x => x.EdgarCompanyDataCik,
                        principalTable: "EdgarCompanyDataList",
                        principalColumn: "Cik");
                });

            migrationBuilder.InsertData(
                table: "EdgarCompanyDataList",
                columns: new[] { "Cik", "EntityName", "Updated", "specialFundableAmount", "standardFundableAmount" },
                values: new object[,]
                {
                    { 6845L, null, null, 0m, 0m },
                    { 14272L, null, null, 0m, 0m },
                    { 18926L, null, null, 0m, 0m },
                    { 21175L, null, null, 0m, 0m },
                    { 52827L, null, null, 0m, 0m },
                    { 60086L, null, null, 0m, 0m },
                    { 64803L, null, null, 0m, 0m },
                    { 92103L, null, null, 0m, 0m },
                    { 217410L, null, null, 0m, 0m },
                    { 310522L, null, null, 0m, 0m },
                    { 312070L, null, null, 0m, 0m },
                    { 314808L, null, null, 0m, 0m },
                    { 726958L, null, null, 0m, 0m },
                    { 730272L, null, null, 0m, 0m },
                    { 823277L, null, null, 0m, 0m },
                    { 831259L, null, null, 0m, 0m },
                    { 844790L, null, null, 0m, 0m },
                    { 882291L, null, null, 0m, 0m },
                    { 884144L, null, null, 0m, 0m },
                    { 884624L, null, null, 0m, 0m },
                    { 892553L, null, null, 0m, 0m },
                    { 895421L, null, null, 0m, 0m },
                    { 914475L, null, null, 0m, 0m },
                    { 915912L, null, null, 0m, 0m },
                    { 923796L, null, null, 0m, 0m },
                    { 927628L, null, null, 0m, 0m },
                    { 933267L, null, null, 0m, 0m },
                    { 947263L, null, null, 0m, 0m },
                    { 1007587L, null, null, 0m, 0m },
                    { 1015647L, null, null, 0m, 0m },
                    { 1034665L, null, null, 0m, 0m },
                    { 1037868L, null, null, 0m, 0m },
                    { 1038074L, null, null, 0m, 0m },
                    { 1046311L, null, null, 0m, 0m },
                    { 1076691L, null, null, 0m, 0m },
                    { 1085277L, null, null, 0m, 0m },
                    { 1108134L, null, null, 0m, 0m },
                    { 1125259L, null, null, 0m, 0m },
                    { 1141788L, null, null, 0m, 0m },
                    { 1157557L, null, null, 0m, 0m },
                    { 1166834L, null, null, 0m, 0m },
                    { 1227857L, null, null, 0m, 0m },
                    { 1231457L, null, null, 0m, 0m },
                    { 1232384L, null, null, 0m, 0m },
                    { 1308106L, null, null, 0m, 0m },
                    { 1323885L, null, null, 0m, 0m },
                    { 1393311L, null, null, 0m, 0m },
                    { 1397183L, null, null, 0m, 0m },
                    { 1400897L, null, null, 0m, 0m },
                    { 1435617L, null, null, 0m, 0m },
                    { 1439124L, null, null, 0m, 0m },
                    { 1447380L, null, null, 0m, 0m },
                    { 1476150L, null, null, 0m, 0m },
                    { 1498382L, null, null, 0m, 0m },
                    { 1501103L, null, null, 0m, 0m },
                    { 1510524L, null, null, 0m, 0m },
                    { 1521036L, null, null, 0m, 0m },
                    { 1526520L, null, null, 0m, 0m },
                    { 1532346L, null, null, 0m, 0m },
                    { 1540159L, null, null, 0m, 0m },
                    { 1541309L, null, null, 0m, 0m },
                    { 1547660L, null, null, 0m, 0m },
                    { 1549922L, null, null, 0m, 0m },
                    { 1550695L, null, null, 0m, 0m },
                    { 1552797L, null, null, 0m, 0m },
                    { 1560293L, null, null, 0m, 0m },
                    { 1634293L, null, null, 0m, 0m },
                    { 1641751L, null, null, 0m, 0m },
                    { 1685428L, null, null, 0m, 0m },
                    { 1691221L, null, null, 0m, 0m },
                    { 1696355L, null, null, 0m, 0m },
                    { 1710350L, null, null, 0m, 0m },
                    { 1729944L, null, null, 0m, 0m },
                    { 1730773L, null, null, 0m, 0m },
                    { 1756708L, null, null, 0m, 0m },
                    { 1757143L, null, null, 0m, 0m },
                    { 1761312L, null, null, 0m, 0m },
                    { 1798562L, null, null, 0m, 0m },
                    { 1819493L, null, null, 0m, 0m },
                    { 1824502L, null, null, 0m, 0m },
                    { 1828248L, null, null, 0m, 0m },
                    { 1843370L, null, null, 0m, 0m },
                    { 1844642L, null, null, 0m, 0m },
                    { 1848898L, null, null, 0m, 0m },
                    { 1849058L, null, null, 0m, 0m },
                    { 1849635L, null, null, 0m, 0m },
                    { 1851182L, null, null, 0m, 0m },
                    { 1853630L, null, null, 0m, 0m },
                    { 1857518L, null, null, 0m, 0m },
                    { 1858007L, null, null, 0m, 0m },
                    { 1858912L, null, null, 0m, 0m },
                    { 1861841L, null, null, 0m, 0m },
                    { 1867287L, null, null, 0m, 0m },
                    { 1872292L, null, null, 0m, 0m },
                    { 1894630L, null, null, 0m, 0m },
                    { 1912498L, null, null, 0m, 0m },
                    { 1958217L, null, null, 0m, 0m },
                    { 1967078L, null, null, 0m, 0m },
                    { 1980088L, null, null, 0m, 0m },
                    { 1988979L, null, null, 0m, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoFactUsGaapIncomeLossUnitsUsd_EdgarCompanyDataCik",
                table: "InfoFactUsGaapIncomeLossUnitsUsd",
                column: "EdgarCompanyDataCik");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoFactUsGaapIncomeLossUnitsUsd");

            migrationBuilder.DropTable(
                name: "EdgarCompanyDataList");
        }
    }
}
