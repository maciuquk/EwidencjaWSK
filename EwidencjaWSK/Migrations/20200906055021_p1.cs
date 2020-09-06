using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class p1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c24a1a70-385f-4956-a60a-74ab08ec278b", "e9594247-3b16-48f7-a9d2-91844da6207e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae613719-84a2-4467-bee7-f66aca2a009c", "9bef8be1-85b9-4127-a2eb-85d6b3ea0a89", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae613719-84a2-4467-bee7-f66aca2a009c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c24a1a70-385f-4956-a60a-74ab08ec278b");
        }
    }
}
