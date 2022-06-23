using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDB.Migrations
{
    public partial class FixPriceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "service_packages",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "hall",
                newName: "price_per_hour");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "employees",
                newName: "price");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "service_packages",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "price_per_hour",
                table: "hall",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "employees",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "service_packages",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "price_per_hour",
                table: "hall",
                newName: "PricePerHour");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "employees",
                newName: "Price");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "service_packages",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerHour",
                table: "hall",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "employees",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
