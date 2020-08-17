using Microsoft.EntityFrameworkCore.Migrations;

namespace EwidencjaWSK.Migrations
{
    public partial class i4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "Parts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Records_RecordId",
                table: "Parts",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
