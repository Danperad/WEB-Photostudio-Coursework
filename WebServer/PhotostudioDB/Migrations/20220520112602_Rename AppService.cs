using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotostudioDB.Migrations
{
    public partial class RenameAppService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_services_address_AddressId",
                table: "application_services");

            migrationBuilder.DropForeignKey(
                name: "FK_application_services_hall_HallId",
                table: "application_services");

            migrationBuilder.DropForeignKey(
                name: "FK_application_services_rented_items_RentedItemId",
                table: "application_services");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "application_services",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "application_services",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "application_services",
                newName: "start_date_time");

            migrationBuilder.RenameColumn(
                name: "RentedItemId",
                table: "application_services",
                newName: "rented_item_id");

            migrationBuilder.RenameColumn(
                name: "IsFullTime",
                table: "application_services",
                newName: "is_full_time");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "application_services",
                newName: "hall_id");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "application_services",
                newName: "address_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_RentedItemId",
                table: "application_services",
                newName: "IX_application_services_rented_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_HallId",
                table: "application_services",
                newName: "IX_application_services_hall_id");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_AddressId",
                table: "application_services",
                newName: "IX_application_services_address_id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_address_address_id",
                table: "application_services",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_hall_hall_id",
                table: "application_services",
                column: "hall_id",
                principalTable: "hall",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_rented_items_rented_item_id",
                table: "application_services",
                column: "rented_item_id",
                principalTable: "rented_items",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_services_address_address_id",
                table: "application_services");

            migrationBuilder.DropForeignKey(
                name: "FK_application_services_hall_hall_id",
                table: "application_services");

            migrationBuilder.DropForeignKey(
                name: "FK_application_services_rented_items_rented_item_id",
                table: "application_services");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "application_services",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "application_services",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "start_date_time",
                table: "application_services",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "rented_item_id",
                table: "application_services",
                newName: "RentedItemId");

            migrationBuilder.RenameColumn(
                name: "is_full_time",
                table: "application_services",
                newName: "IsFullTime");

            migrationBuilder.RenameColumn(
                name: "hall_id",
                table: "application_services",
                newName: "HallId");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "application_services",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_rented_item_id",
                table: "application_services",
                newName: "IX_application_services_RentedItemId");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_hall_id",
                table: "application_services",
                newName: "IX_application_services_HallId");

            migrationBuilder.RenameIndex(
                name: "IX_application_services_address_id",
                table: "application_services",
                newName: "IX_application_services_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_address_AddressId",
                table: "application_services",
                column: "AddressId",
                principalTable: "address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_hall_HallId",
                table: "application_services",
                column: "HallId",
                principalTable: "hall",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_application_services_rented_items_RentedItemId",
                table: "application_services",
                column: "RentedItemId",
                principalTable: "rented_items",
                principalColumn: "id");
        }
    }
}
