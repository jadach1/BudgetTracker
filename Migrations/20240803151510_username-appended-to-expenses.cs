using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class usernameappendedtoexpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<string>(
                name: "bigName",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "crazyName",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_crazyName",
                table: "Expenses",
                column: "crazyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_crazyName",
                table: "Expenses",
                column: "crazyName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_crazyName",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_crazyName",
                table: "Expenses");


            migrationBuilder.DropColumn(
                name: "bigName",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "crazyName",
                table: "Expenses");

        
        }
    }
}
