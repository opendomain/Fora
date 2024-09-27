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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                columns: new[] { "Cik", "EntityName" },
                values: new object[,]
                {
                    { 6845, null },
                    { 14272, null },
                    { 18926, null },
                    { 21175, null },
                    { 52827, null },
                    { 60086, null },
                    { 64803, null },
                    { 92103, null },
                    { 217410, null },
                    { 310522, null },
                    { 312070, null },
                    { 314808, null },
                    { 726958, null },
                    { 730272, null },
                    { 823277, null },
                    { 831259, null },
                    { 844790, null },
                    { 882291, null },
                    { 884144, null },
                    { 884624, null },
                    { 892553, null },
                    { 895421, null },
                    { 914475, null },
                    { 915912, null },
                    { 923796, null },
                    { 927628, null },
                    { 933267, null },
                    { 947263, null },
                    { 1007587, null },
                    { 1015647, null },
                    { 1034665, null },
                    { 1037868, null },
                    { 1038074, null },
                    { 1046311, null },
                    { 1076691, null },
                    { 1085277, null },
                    { 1108134, null },
                    { 1125259, null },
                    { 1141788, null },
                    { 1157557, null },
                    { 1166834, null },
                    { 1227857, null },
                    { 1231457, null },
                    { 1232384, null },
                    { 1308106, null },
                    { 1323885, null },
                    { 1393311, null },
                    { 1397183, null },
                    { 1400897, null },
                    { 1435617, null },
                    { 1439124, null },
                    { 1447380, null },
                    { 1476150, null },
                    { 1498382, null },
                    { 1501103, null },
                    { 1510524, null },
                    { 1521036, null },
                    { 1526520, null },
                    { 1532346, null },
                    { 1540159, null },
                    { 1541309, null },
                    { 1547660, null },
                    { 1549922, null },
                    { 1550695, null },
                    { 1552797, null },
                    { 1560293, null },
                    { 1634293, null },
                    { 1641751, null },
                    { 1685428, null },
                    { 1691221, null },
                    { 1696355, null },
                    { 1710350, null },
                    { 1729944, null },
                    { 1730773, null },
                    { 1756708, null },
                    { 1757143, null },
                    { 1761312, null },
                    { 1798562, null },
                    { 1819493, null },
                    { 1824502, null },
                    { 1828248, null },
                    { 1843370, null },
                    { 1844642, null },
                    { 1848898, null },
                    { 1849058, null },
                    { 1849635, null },
                    { 1851182, null },
                    { 1853630, null },
                    { 1857518, null },
                    { 1858007, null },
                    { 1858912, null },
                    { 1861841, null },
                    { 1867287, null },
                    { 1872292, null },
                    { 1894630, null },
                    { 1912498, null },
                    { 1958217, null },
                    { 1967078, null },
                    { 1980088, null },
                    { 1988979, null }
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
