using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exomine.Migrations
{
    /// <inheritdoc />
    public partial class honestlyidontknow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6,
                column: "MaxTimeSeconds",
                value: 120);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6,
                column: "MaxTimeSeconds",
                value: 30);
        }
    }
}
