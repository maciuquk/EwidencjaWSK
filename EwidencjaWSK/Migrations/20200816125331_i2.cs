using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class i2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Suppliers_SupplierId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_SupplierId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Records",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RecordSupplierId",
                table: "Records",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_RecordSupplierId",
                table: "Records",
                column: "RecordSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Suppliers_RecordSupplierId",
                table: "Records",
                column: "RecordSupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Suppliers_RecordSupplierId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_RecordSupplierId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "RecordSupplierId",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Records",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "Parts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Records_SupplierId",
                table: "Records",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Suppliers_SupplierId",
                table: "Records",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
