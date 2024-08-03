using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class usernameappendedtoexpenses2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_crazyName",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "bigName",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "crazyName",
                table: "Expenses",
                newName: "myUserName");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_crazyName",
                table: "Expenses",
                newName: "IX_Expenses_myUserName");


            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_myUserName",
                table: "Expenses",
                column: "myUserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_myUserName",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "myUserName",
                table: "Expenses",
                newName: "crazyName");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_myUserName",
                table: "Expenses",
                newName: "IX_Expenses_crazyName");

            migrationBuilder.AddColumn<string>(
                name: "bigName",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_crazyName",
                table: "Expenses",
                column: "crazyName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
