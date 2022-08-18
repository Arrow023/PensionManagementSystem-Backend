using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthentication.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AccountNumber",
                table: "Banks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thread = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "AccountNumber", "Type" },
                values: new object[,]
                {
                    { 1, 9876543210L, 0 },
                    { 2, 7894561230L, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "VXNlcjE=", "User1" },
                    { 2, "VXNlcjI=", "User2" }
                });

            migrationBuilder.InsertData(
                table: "PensionerDetails",
                columns: new[] { "UserId", "AadhaarNumber", "Allowances", "BankId", "DateOfBirth", "Name", "PAN", "SalaryEarned", "Type" },
                values: new object[] { 1, 1234567898012L, 5000.0, 1, new DateTime(2022, 8, 10, 16, 20, 57, 457, DateTimeKind.Local).AddTicks(7615), "User one", "PAN9876543", 30000.0, 0 });

            migrationBuilder.InsertData(
                table: "PensionerDetails",
                columns: new[] { "UserId", "AadhaarNumber", "Allowances", "BankId", "DateOfBirth", "Name", "PAN", "SalaryEarned", "Type" },
                values: new object[] { 2, 1234567898013L, 4500.0, 2, new DateTime(2022, 8, 11, 16, 20, 57, 457, DateTimeKind.Local).AddTicks(7633), "User two", "PAN7894562", 32000.0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "PensionerDetails",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PensionerDetails",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Banks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
