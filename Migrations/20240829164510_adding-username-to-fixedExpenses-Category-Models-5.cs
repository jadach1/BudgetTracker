using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addingusernametofixedExpensesCategoryModels5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FixedExpense",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Categories",
                newName: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "FixedExpense",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Categories",
                newName: "UserId");
        }
    }
}
