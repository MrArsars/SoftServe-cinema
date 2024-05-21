using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedplacesindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "ReservedPlaces",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedPlaces",
                table: "Sessions");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Places",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
