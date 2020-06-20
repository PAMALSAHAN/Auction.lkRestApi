using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class addColorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "vehiclesTbl",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "vehiclesTbl");
        }
    }
}
