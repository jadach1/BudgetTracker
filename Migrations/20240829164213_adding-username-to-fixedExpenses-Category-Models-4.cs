using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addingusernametofixedExpensesCategoryModels4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "FixedExpense",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Categories",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FixedExpense",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Categories",
                newName: "UserName");
        }
    }
}
