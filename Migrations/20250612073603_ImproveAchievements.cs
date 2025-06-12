using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exomine.Migrations
{
    /// <inheritdoc />
    public partial class ImproveAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Win a hexagon game at difficulty at least 25");

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 60, 10, "Win a hexagon game at difficulty at least 25 under 60 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 25, "Win a square game at difficulty at least 25" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 120, 25, "Win a square game at difficulty at least 25 under 120 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 30, "Win a triangle game at difficulty at least 30" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 300, 30, "Win a triangle game at difficulty at least 30 under 300 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 25, "Win a squareTriHex game at difficulty at least 25" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 180, 25, "Win a squareTriHex game at difficulty at least 25 under 180 seconds" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Win a hexagon game at difficulty at least 20");

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 120, 20, "Win a hexagon game at difficulty at least 20 under 30 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 20, "Win a square game at difficulty at least 20" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 30, 20, "Win a square game at difficulty at least 20 under 30 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 20, "Win a triangle game at difficulty at least 20" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 30, 20, "Win a triangle game at difficulty at least 20 under 30 seconds" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "MinDifficulty", "Name" },
                values: new object[] { 20, "Win a squareTriHex game at difficulty at least 20" });

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "MaxTimeSeconds", "MinDifficulty", "Name" },
                values: new object[] { 30, 20, "Win a squareTriHex game at difficulty at least 20 under 30 seconds" });
        }
    }
}
