using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "058fae4b-1768-40f2-a802-93f92e9de2a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d4e4c1-5903-40a0-ac7d-3b16d1eea972");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0caa18af-670f-4e6c-a8bc-38214f69db28", null, "Visitor", "VISITOR" },
                    { "9c75fab2-ff16-495e-a484-d0dfcfd4fd94", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0caa18af-670f-4e6c-a8bc-38214f69db28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c75fab2-ff16-495e-a484-d0dfcfd4fd94");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "058fae4b-1768-40f2-a802-93f92e9de2a4", null, "Visitor", "VISITOR" },
                    { "c1d4e4c1-5903-40a0-ac7d-3b16d1eea972", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
