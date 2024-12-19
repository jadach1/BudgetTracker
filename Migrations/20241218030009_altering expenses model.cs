using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class alteringexpensesmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFixed",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFixed",
                table: "Expenses");
        }
    }
}
