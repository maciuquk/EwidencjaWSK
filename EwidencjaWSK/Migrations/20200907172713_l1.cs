using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class l1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae613719-84a2-4467-bee7-f66aca2a009c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c24a1a70-385f-4956-a60a-74ab08ec278b");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Suppliers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Suppliers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Parts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Parts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AdditionalDocs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AdditionalDocs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "AdditionalDocs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AdditionalDocs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "610021ab-95e4-4571-8cde-ad2a9b40a44b", "5d2a2da0-9b70-4878-8318-7eedbb718444", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f44614b-b01b-40c7-8170-c68768e1a380", "86c46510-01cc-43a9-9299-1deb821195fe", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f44614b-b01b-40c7-8170-c68768e1a380");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610021ab-95e4-4571-8cde-ad2a9b40a44b");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AdditionalDocs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AdditionalDocs");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "AdditionalDocs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AdditionalDocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c24a1a70-385f-4956-a60a-74ab08ec278b", "e9594247-3b16-48f7-a9d2-91844da6207e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae613719-84a2-4467-bee7-f66aca2a009c", "9bef8be1-85b9-4127-a2eb-85d6b3ea0a89", "Admin", "ADMIN" });
        }
    }
}
