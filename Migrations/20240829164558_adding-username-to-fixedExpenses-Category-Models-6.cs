using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addingusernametofixedExpensesCategoryModels6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "FixedExpense",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Categories",
                newName: "Userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "FixedExpense",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Categories",
                newName: "User_Id");
        }
    }
}
