using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class i2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalDocs",
                columns: table => new
                {
                    AdditionalDocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    KindOfDoc = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalDocs", x => x.AdditionalDocId);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsInArmedList = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    KindOfTransaction = table.Column<string>(nullable: true),
                    IsCheckDenyList = table.Column<bool>(nullable: false),
                    IsCheckWarningSignalList = table.Column<bool>(nullable: false),
                    IsInPartsBase = table.Column<bool>(nullable: false),
                    IsNecessaryMinistryPermit = table.Column<bool>(nullable: false),
                    SuplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Records_Suppliers_SuplierId",
                        column: x => x.SuplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsAdditionalDocs",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false),
                    AdditionalDocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsAdditionalDocs", x => new { x.RecordId, x.AdditionalDocId });
                    table.ForeignKey(
                        name: "FK_RecordsAdditionalDocs_AdditionalDocs_AdditionalDocId",
                        column: x => x.AdditionalDocId,
                        principalTable: "AdditionalDocs",
                        principalColumn: "AdditionalDocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordsAdditionalDocs_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsParts",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false),
                    PartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsParts", x => new { x.RecordId, x.PartId });
                    table.ForeignKey(
                        name: "FK_RecordsParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordsParts_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_SuplierId",
                table: "Records",
                column: "SuplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsAdditionalDocs_AdditionalDocId",
                table: "RecordsAdditionalDocs",
                column: "AdditionalDocId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsParts_PartId",
                table: "RecordsParts",
                column: "PartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordsAdditionalDocs");

            migrationBuilder.DropTable(
                name: "RecordsParts");

            migrationBuilder.DropTable(
                name: "AdditionalDocs");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
