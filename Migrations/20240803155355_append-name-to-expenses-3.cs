using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budget_Man.Migrations
{
    /// <inheritdoc />
    public partial class appendnametoexpenses3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_myUserName",
                table: "Expenses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a7fef7c-ec6c-495f-a3c6-39b25ef40067");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa1a3349-10cb-43e0-a935-8409a8cfd9d3");

            migrationBuilder.RenameColumn(
                name: "myUserName",
                table: "Expenses",
                newName: "MyUserName");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_myUserName",
                table: "Expenses",
                newName: "IX_Expenses_MyUserName");

            migrationBuilder.AlterColumn<string>(
                name: "MyUserName",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a14a7ce4-ae72-46b4-9d6a-d9bec383ffd7", null, "Administrator", "ADMINISTRATOR" },
                    { "ad89333b-1219-486d-9619-9516053d31fe", null, "Visitor", "VISITOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_MyUserName",
                table: "Expenses",
                column: "MyUserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_MyUserName",
                table: "Expenses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a14a7ce4-ae72-46b4-9d6a-d9bec383ffd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad89333b-1219-486d-9619-9516053d31fe");

            migrationBuilder.RenameColumn(
                name: "MyUserName",
                table: "Expenses",
                newName: "myUserName");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_MyUserName",
                table: "Expenses",
                newName: "IX_Expenses_myUserName");

            migrationBuilder.AlterColumn<string>(
                name: "myUserName",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "290ff56a-0943-4692-b6e4-10d3219fbb9a", null, "Visitor", "VISITOR" },
                    { "e7043ca0-cbf9-4fd8-8eb4-9fcaeb359ffc", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_myUserName",
                table: "Expenses",
                column: "myUserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
