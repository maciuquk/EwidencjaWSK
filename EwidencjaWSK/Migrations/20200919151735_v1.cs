using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28671b48-adc5-44bb-9c71-6d4404e4d3c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99e522ed-9d2a-4475-9d8d-2e8288a2742c");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Records",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1351d2a-f810-4e16-ac98-ad6e900d8044", "4aa17f38-dd02-43fb-a647-57a98ba3c330", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "808b1d80-c194-4770-967f-1c95f2bd640e", "e2623589-42bf-408b-ace3-437c815be1ef", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "808b1d80-c194-4770-967f-1c95f2bd640e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1351d2a-f810-4e16-ac98-ad6e900d8044");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Records",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99e522ed-9d2a-4475-9d8d-2e8288a2742c", "7da9cb5f-5e15-4e28-a5ca-6e08b5d1ee94", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28671b48-adc5-44bb-9c71-6d4404e4d3c9", "6aba7a84-dab0-4014-a8ae-c4d43423c76c", "Admin", "ADMIN" });
        }
    }
}
