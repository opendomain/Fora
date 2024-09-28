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
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                columns: new[] { "Cik", "EntityName", "Updated" },
                values: new object[,]
                {
                    { 6845L, null, null },
                    { 14272L, null, null },
                    { 18926L, null, null },
                    { 21175L, null, null },
                    { 52827L, null, null },
                    { 60086L, null, null },
                    { 64803L, null, null },
                    { 92103L, null, null },
                    { 217410L, null, null },
                    { 310522L, null, null },
                    { 312070L, null, null },
                    { 314808L, null, null },
                    { 726958L, null, null },
                    { 730272L, null, null },
                    { 823277L, null, null },
                    { 831259L, null, null },
                    { 844790L, null, null },
                    { 882291L, null, null },
                    { 884144L, null, null },
                    { 884624L, null, null },
                    { 892553L, null, null },
                    { 895421L, null, null },
                    { 914475L, null, null },
                    { 915912L, null, null },
                    { 923796L, null, null },
                    { 927628L, null, null },
                    { 933267L, null, null },
                    { 947263L, null, null },
                    { 1007587L, null, null },
                    { 1015647L, null, null },
                    { 1034665L, null, null },
                    { 1037868L, null, null },
                    { 1038074L, null, null },
                    { 1046311L, null, null },
                    { 1076691L, null, null },
                    { 1085277L, null, null },
                    { 1108134L, null, null },
                    { 1125259L, null, null },
                    { 1141788L, null, null },
                    { 1157557L, null, null },
                    { 1166834L, null, null },
                    { 1227857L, null, null },
                    { 1231457L, null, null },
                    { 1232384L, null, null },
                    { 1308106L, null, null },
                    { 1323885L, null, null },
                    { 1393311L, null, null },
                    { 1397183L, null, null },
                    { 1400897L, null, null },
                    { 1435617L, null, null },
                    { 1439124L, null, null },
                    { 1447380L, null, null },
                    { 1476150L, null, null },
                    { 1498382L, null, null },
                    { 1501103L, null, null },
                    { 1510524L, null, null },
                    { 1521036L, null, null },
                    { 1526520L, null, null },
                    { 1532346L, null, null },
                    { 1540159L, null, null },
                    { 1541309L, null, null },
                    { 1547660L, null, null },
                    { 1549922L, null, null },
                    { 1550695L, null, null },
                    { 1552797L, null, null },
                    { 1560293L, null, null },
                    { 1634293L, null, null },
                    { 1641751L, null, null },
                    { 1685428L, null, null },
                    { 1691221L, null, null },
                    { 1696355L, null, null },
                    { 1710350L, null, null },
                    { 1729944L, null, null },
                    { 1730773L, null, null },
                    { 1756708L, null, null },
                    { 1757143L, null, null },
                    { 1761312L, null, null },
                    { 1798562L, null, null },
                    { 1819493L, null, null },
                    { 1824502L, null, null },
                    { 1828248L, null, null },
                    { 1843370L, null, null },
                    { 1844642L, null, null },
                    { 1848898L, null, null },
                    { 1849058L, null, null },
                    { 1849635L, null, null },
                    { 1851182L, null, null },
                    { 1853630L, null, null },
                    { 1857518L, null, null },
                    { 1858007L, null, null },
                    { 1858912L, null, null },
                    { 1861841L, null, null },
                    { 1867287L, null, null },
                    { 1872292L, null, null },
                    { 1894630L, null, null },
                    { 1912498L, null, null },
                    { 1958217L, null, null },
                    { 1967078L, null, null },
                    { 1980088L, null, null },
                    { 1988979L, null, null }
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
