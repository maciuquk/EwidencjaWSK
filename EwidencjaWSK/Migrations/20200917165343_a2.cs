using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03fe8219-34f2-4e82-b3c4-1c2d1752246b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f7c49ea-e005-408d-be0e-3377208a3331");

            migrationBuilder.AddColumn<string>(
                name: "ChangedBy",
                table: "Audits",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99e522ed-9d2a-4475-9d8d-2e8288a2742c", "7da9cb5f-5e15-4e28-a5ca-6e08b5d1ee94", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28671b48-adc5-44bb-9c71-6d4404e4d3c9", "6aba7a84-dab0-4014-a8ae-c4d43423c76c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28671b48-adc5-44bb-9c71-6d4404e4d3c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99e522ed-9d2a-4475-9d8d-2e8288a2742c");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
                table: "Audits");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f7c49ea-e005-408d-be0e-3377208a3331", "425d1981-5434-4847-96dc-564b97d0508e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03fe8219-34f2-4e82-b3c4-1c2d1752246b", "35bc605a-a4b9-4ce6-a3f2-c1ed277c73a8", "Admin", "ADMIN" });
        }
    }
}
