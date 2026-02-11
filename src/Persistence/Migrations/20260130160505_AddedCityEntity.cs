using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    IsCapital = table.Column<bool>(type: "bit", nullable: false),
                    IsTouristic = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FlagUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Elevation = table.Column<int>(type: "int", nullable: false),
                    GDP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Density = table.Column<double>(type: "float", nullable: false),
                    TransportSystem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AirportsCount = table.Column<int>(type: "int", nullable: false),
                    UniversitiesCount = table.Column<int>(type: "int", nullable: false),
                    HospitalsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsCapital",
                table: "Cities",
                column: "IsCapital",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
