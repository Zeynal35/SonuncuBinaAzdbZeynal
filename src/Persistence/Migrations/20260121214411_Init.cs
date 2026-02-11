using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyAd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false),
                    IsMortgage = table.Column<bool>(type: "bit", nullable: false),
                    IsExtract = table.Column<bool>(type: "bit", nullable: false),
                    OfferType = table.Column<int>(type: "int", nullable: false),
                    RealEstateType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PropertyAdId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyMedia_PropertyAd_PropertyAdId",
                        column: x => x.PropertyAdId,
                        principalTable: "PropertyAd",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMedia_PropertyAdId",
                table: "PropertyMedia",
                column: "PropertyAdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMedia");

            migrationBuilder.DropTable(
                name: "PropertyAd");
        }
    }
}
