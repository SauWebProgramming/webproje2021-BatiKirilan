using Microsoft.EntityFrameworkCore.Migrations;

namespace manav.Migrations
{
    public partial class son : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MüsteriAdi",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "MüsteriEmail",
                table: "Siparisler");

            migrationBuilder.AlterColumn<double>(
                name: "SiparisTutari",
                table: "Siparisler",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MüsteriId",
                table: "Siparisler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_MüsteriId",
                table: "Siparisler",
                column: "MüsteriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_AspNetUsers_MüsteriId",
                table: "Siparisler",
                column: "MüsteriId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_AspNetUsers_MüsteriId",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_MüsteriId",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "MüsteriId",
                table: "Siparisler");

            migrationBuilder.AlterColumn<int>(
                name: "SiparisTutari",
                table: "Siparisler",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "MüsteriAdi",
                table: "Siparisler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MüsteriEmail",
                table: "Siparisler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
