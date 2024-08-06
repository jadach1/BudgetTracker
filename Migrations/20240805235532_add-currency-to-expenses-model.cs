using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class addcurrencytoexpensesmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a14a7ce4-ae72-46b4-9d6a-d9bec383ffd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad89333b-1219-486d-9619-9516053d31fe");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "657ecbe3-82d1-4203-b581-6bfc69c35f74", null, "Visitor", "VISITOR" },
                    { "ee413926-253f-49bd-8154-e3b7386dab12", null, "Administrator", "ADMINISTRATOR" }
                });

                migrationBuilder.AlterColumn<string>(
                name: "MyUserName",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "657ecbe3-82d1-4203-b581-6bfc69c35f74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee413926-253f-49bd-8154-e3b7386dab12");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a14a7ce4-ae72-46b4-9d6a-d9bec383ffd7", null, "Administrator", "ADMINISTRATOR" },
                    { "ad89333b-1219-486d-9619-9516053d31fe", null, "Visitor", "VISITOR" }
                });
        }
    }
}
