using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace exomine.Migrations
{
    /// <inheritdoc />
    public partial class Achievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GridType = table.Column<int>(type: "int", nullable: true),
                    MinSize = table.Column<int>(type: "int", nullable: true),
                    MinDifficulty = table.Column<int>(type: "int", nullable: true),
                    MaxTimeSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievement",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievement", x => new { x.UserId, x.AchievementId });
                    table.ForeignKey(
                        name: "FK_UserAchievement_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievement_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Achievement",
                columns: new[] { "Id", "GridType", "MaxTimeSeconds", "MinDifficulty", "MinSize", "Name" },
                values: new object[,]
                {
                    { 1, null, null, null, 5, "Win any game at least size 5" },
                    { 2, null, null, null, 10, "Win any game at least size 10" },
                    { 3, 0, null, null, 5, "Win a hexagon game at size 5" },
                    { 4, 0, null, null, 10, "Win a hexagon game at size 10" },
                    { 5, 0, null, 20, null, "Win a hexagon game at difficulty at least 20" },
                    { 6, 0, 30, 20, null, "Win a hexagon game at difficulty at least 20 under 30 seconds" },
                    { 7, 1, null, null, 5, "Win a square game at size 5" },
                    { 8, 1, null, null, 10, "Win a square game at size 10" },
                    { 9, 1, null, 20, null, "Win a square game at difficulty at least 20" },
                    { 10, 1, 30, 20, null, "Win a square game at difficulty at least 20 under 30 seconds" },
                    { 11, 2, null, null, 5, "Win a triangle game at size 5" },
                    { 12, 2, null, null, 10, "Win a triangle game at size 10" },
                    { 13, 2, null, 20, null, "Win a triangle game at difficulty at least 20" },
                    { 14, 2, 30, 20, null, "Win a triangle game at difficulty at least 20 under 30 seconds" },
                    { 15, 3, null, null, 5, "Win a squareTriHex game at size 5" },
                    { 16, 3, null, null, 10, "Win a squareTriHex game at size 10" },
                    { 17, 3, null, 20, null, "Win a squareTriHex game at difficulty at least 20" },
                    { 18, 3, 30, 20, null, "Win a squareTriHex game at difficulty at least 20 under 30 seconds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_AchievementId",
                table: "UserAchievement",
                column: "AchievementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievement");

            migrationBuilder.DropTable(
                name: "Achievement");
        }
    }
}
