using Microsoft.EntityFrameworkCore.Migrations;

namespace manav.Migrations
{
    public partial class son1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MüsteriTel",
                table: "Siparisler");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MüsteriTel",
                table: "Siparisler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
