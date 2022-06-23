using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDB.Migrations
{
    public partial class AddCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "cost",
                table: "services",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "service_packages",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "hall",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "service_packages");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "hall");

            migrationBuilder.AlterColumn<decimal>(
                name: "cost",
                table: "services",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
