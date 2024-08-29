using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addingusernametofixedExpensesCategoryModels3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71a2b8b2-af83-458e-ba21-29221585cfe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f229686c-c7e2-46c0-8a63-467c72dbe5d1");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "FixedExpense",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "FixedExpense");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71a2b8b2-af83-458e-ba21-29221585cfe8", null, "Administrator", "ADMINISTRATOR" },
                    { "f229686c-c7e2-46c0-8a63-467c72dbe5d1", null, "Visitor", "VISITOR" }
                });
        }
    }
}
