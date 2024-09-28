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
                    Cik = table.Column<int>(type: "int", nullable: false),
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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Val = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EdgarCompanyDataCik = table.Column<int>(type: "int", nullable: true)
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
                    { 6845, null, null },
                    { 14272, null, null },
                    { 18926, null, null },
                    { 21175, null, null },
                    { 52827, null, null },
                    { 60086, null, null },
                    { 64803, null, null },
                    { 92103, null, null },
                    { 217410, null, null },
                    { 310522, null, null },
                    { 312070, null, null },
                    { 314808, null, null },
                    { 726958, null, null },
                    { 730272, null, null },
                    { 823277, null, null },
                    { 831259, null, null },
                    { 844790, null, null },
                    { 882291, null, null },
                    { 884144, null, null },
                    { 884624, null, null },
                    { 892553, null, null },
                    { 895421, null, null },
                    { 914475, null, null },
                    { 915912, null, null },
                    { 923796, null, null },
                    { 927628, null, null },
                    { 933267, null, null },
                    { 947263, null, null },
                    { 1007587, null, null },
                    { 1015647, null, null },
                    { 1034665, null, null },
                    { 1037868, null, null },
                    { 1038074, null, null },
                    { 1046311, null, null },
                    { 1076691, null, null },
                    { 1085277, null, null },
                    { 1108134, null, null },
                    { 1125259, null, null },
                    { 1141788, null, null },
                    { 1157557, null, null },
                    { 1166834, null, null },
                    { 1227857, null, null },
                    { 1231457, null, null },
                    { 1232384, null, null },
                    { 1308106, null, null },
                    { 1323885, null, null },
                    { 1393311, null, null },
                    { 1397183, null, null },
                    { 1400897, null, null },
                    { 1435617, null, null },
                    { 1439124, null, null },
                    { 1447380, null, null },
                    { 1476150, null, null },
                    { 1498382, null, null },
                    { 1501103, null, null },
                    { 1510524, null, null },
                    { 1521036, null, null },
                    { 1526520, null, null },
                    { 1532346, null, null },
                    { 1540159, null, null },
                    { 1541309, null, null },
                    { 1547660, null, null },
                    { 1549922, null, null },
                    { 1550695, null, null },
                    { 1552797, null, null },
                    { 1560293, null, null },
                    { 1634293, null, null },
                    { 1641751, null, null },
                    { 1685428, null, null },
                    { 1691221, null, null },
                    { 1696355, null, null },
                    { 1710350, null, null },
                    { 1729944, null, null },
                    { 1730773, null, null },
                    { 1756708, null, null },
                    { 1757143, null, null },
                    { 1761312, null, null },
                    { 1798562, null, null },
                    { 1819493, null, null },
                    { 1824502, null, null },
                    { 1828248, null, null },
                    { 1843370, null, null },
                    { 1844642, null, null },
                    { 1848898, null, null },
                    { 1849058, null, null },
                    { 1849635, null, null },
                    { 1851182, null, null },
                    { 1853630, null, null },
                    { 1857518, null, null },
                    { 1858007, null, null },
                    { 1858912, null, null },
                    { 1861841, null, null },
                    { 1867287, null, null },
                    { 1872292, null, null },
                    { 1894630, null, null },
                    { 1912498, null, null },
                    { 1958217, null, null },
                    { 1967078, null, null },
                    { 1980088, null, null },
                    { 1988979, null, null }
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
