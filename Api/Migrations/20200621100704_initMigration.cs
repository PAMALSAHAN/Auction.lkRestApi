using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehiclesTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Engine = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    IsHotandNew = table.Column<bool>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiclesTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehiclesTbl_CategoryTbl_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehiclesTbl_UserTbl_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageTbl_vehiclesTbl_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehiclesTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageTbl_VehicleId",
                table: "ImageTbl",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_vehiclesTbl_CategoryId",
                table: "vehiclesTbl",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_vehiclesTbl_UserId",
                table: "vehiclesTbl",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageTbl");

            migrationBuilder.DropTable(
                name: "vehiclesTbl");

            migrationBuilder.DropTable(
                name: "CategoryTbl");

            migrationBuilder.DropTable(
                name: "UserTbl");
        }
    }
}
