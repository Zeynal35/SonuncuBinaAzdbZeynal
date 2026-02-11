using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PropertyMediaObjectKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "PropertyMedia");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "PropertyMedia",
                newName: "ObjectKey");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PropertyMedia",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PropertyMedia");

            migrationBuilder.RenameColumn(
                name: "ObjectKey",
                table: "PropertyMedia",
                newName: "MediaUrl");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "PropertyMedia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
