using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class incomeisIncome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isIncome",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isIncome",
                table: "Expenses");
        }
    }
}
