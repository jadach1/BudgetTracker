using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class currencytofixedexpensesmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "657ecbe3-82d1-4203-b581-6bfc69c35f74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee413926-253f-49bd-8154-e3b7386dab12");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "FixedExpense",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d4aa9dc-ed35-4473-8aba-e7666312f3d5", null, "Visitor", "VISITOR" },
                    { "b8b50878-cd78-4edc-bca9-58691a8fbc8c", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "FixedExpense",
                keyColumn: "Id",
                keyValue: 1,
                column: "Currency",
                value: "USD");

            migrationBuilder.UpdateData(
                table: "FixedExpense",
                keyColumn: "Id",
                keyValue: 2,
                column: "Currency",
                value: "USD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d4aa9dc-ed35-4473-8aba-e7666312f3d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8b50878-cd78-4edc-bca9-58691a8fbc8c");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "FixedExpense");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "657ecbe3-82d1-4203-b581-6bfc69c35f74", null, "Visitor", "VISITOR" },
                    { "ee413926-253f-49bd-8154-e3b7386dab12", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
