using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RecordId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_PartId",
                table: "Parts",
                column: "PartId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_PartId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "Parts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_RecordId",
                table: "Parts",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
