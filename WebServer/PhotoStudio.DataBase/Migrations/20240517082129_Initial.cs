using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhotoStudio.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ApplicationServiceTemplateSequence");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city_district = table.Column<string>(type: "text", nullable: true),
                    settlement = table.Column<string>(type: "text", nullable: true),
                    street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    house_number = table.Column<string>(type: "text", nullable: false),
                    block = table.Column<string>(type: "text", nullable: true),
                    apartment_number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rented_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rented_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    photos = table.Column<List<string>>(type: "text[]", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => new { x.id, x.type });
                });

            migrationBuilder.CreateTable(
                name: "hall",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    price_per_hour = table.Column<decimal>(type: "money", nullable: false),
                    photos = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hall", x => x.id);
                    table.ForeignKey(
                        name: "FK_hall_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    token = table.Column<string>(type: "text", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    sign_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.token);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    passport = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeService",
                columns: table => new
                {
                    BoundEmployeesId = table.Column<int>(type: "integer", nullable: false),
                    BoundServicesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeService", x => new { x.BoundEmployeesId, x.BoundServicesId });
                    table.ForeignKey(
                        name: "FK_EmployeeService_employees_BoundEmployeesId",
                        column: x => x.BoundEmployeesId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeService_services_BoundServicesId",
                        column: x => x.BoundServicesId,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_packages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    photos = table.Column<List<string>>(type: "text[]", nullable: false),
                    hall_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_packages", x => x.id);
                    table.ForeignKey(
                        name: "FK_service_packages_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_packages_hall_hall_id",
                        column: x => x.hall_id,
                        principalTable: "hall",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_packages_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "application_services_templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"ApplicationServiceTemplateSequence\"')"),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    hall_id = table.Column<int>(type: "integer", nullable: true),
                    rented_item_id = table.Column<int>(type: "integer", nullable: true),
                    number = table.Column<int>(type: "integer", nullable: true),
                    is_full_time = table.Column<bool>(type: "boolean", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_type = table.Column<int>(type: "integer", nullable: false),
                    StylistId = table.Column<int>(type: "integer", nullable: true),
                    ServicePackageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_services_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_application_services_templates_employees_StylistId",
                        column: x => x.StylistId,
                        principalTable: "employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_templates_hall_hall_id",
                        column: x => x.hall_id,
                        principalTable: "hall",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_templates_rented_items_rented_item_id",
                        column: x => x.rented_item_id,
                        principalTable: "rented_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_templates_service_packages_ServicePack~",
                        column: x => x.ServicePackageId,
                        principalTable: "service_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_services_templates_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_services_templates_statuses_status_id_status_ty~",
                        columns: x => new { x.status_id, x.status_type },
                        principalTable: "statuses",
                        principalColumns: new[] { "id", "type" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_type = table.Column<int>(type: "integer", nullable: false),
                    service_package_id = table.Column<int>(type: "integer", nullable: true),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_service_packages_service_package_id",
                        column: x => x.service_package_id,
                        principalTable: "service_packages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_statuses_status_id_status_type",
                        columns: x => new { x.status_id, x.status_type },
                        principalTable: "statuses",
                        principalColumns: new[] { "id", "type" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_services",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_type = table.Column<int>(type: "integer", nullable: false),
                    start_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    hall_id = table.Column<int>(type: "integer", nullable: true),
                    rented_item_id = table.Column<int>(type: "integer", nullable: true),
                    number = table.Column<int>(type: "integer", nullable: true),
                    is_full_time = table.Column<bool>(type: "boolean", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    BingingPackageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_application_services_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_hall_hall_id",
                        column: x => x.hall_id,
                        principalTable: "hall",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_services_rented_items_rented_item_id",
                        column: x => x.rented_item_id,
                        principalTable: "rented_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_service_packages_BingingPackageId",
                        column: x => x.BingingPackageId,
                        principalTable: "service_packages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_application_services_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_services_statuses_status_id_status_type",
                        columns: x => new { x.status_id, x.status_type },
                        principalTable: "statuses",
                        principalColumns: new[] { "id", "type" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.id);
                    table.ForeignKey(
                        name: "FK_contracts_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contracts_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contracts_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_services_BingingPackageId",
                table: "application_services",
                column: "BingingPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_employee_id",
                table: "application_services",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_hall_id",
                table: "application_services",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_order_id",
                table: "application_services",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_rented_item_id",
                table: "application_services",
                column: "rented_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_service_id",
                table: "application_services",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_status_id_status_type",
                table: "application_services",
                columns: new[] { "status_id", "status_type" });

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_hall_id",
                table: "application_services_templates",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_rented_item_id",
                table: "application_services_templates",
                column: "rented_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_service_id",
                table: "application_services_templates",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_ServicePackageId",
                table: "application_services_templates",
                column: "ServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_status_id_status_type",
                table: "application_services_templates",
                columns: new[] { "status_id", "status_type" });

            migrationBuilder.CreateIndex(
                name: "IX_application_services_templates_StylistId",
                table: "application_services_templates",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_clients_phone",
                table: "clients",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contracts_client_id",
                table: "contracts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_employee_id",
                table: "contracts",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_order_id",
                table: "contracts",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_passport",
                table: "employees",
                column: "passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_phone",
                table: "employees",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_role_id",
                table: "employees",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeService_BoundServicesId",
                table: "EmployeeService",
                column: "BoundServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_hall_address_id",
                table: "hall",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_hall_title",
                table: "hall",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_service_package_id",
                table: "orders",
                column: "service_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_status_id_status_type",
                table: "orders",
                columns: new[] { "status_id", "status_type" });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_client_id",
                table: "refresh_tokens",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_rented_items_title",
                table: "rented_items",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_title",
                table: "roles",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_service_packages_employee_id",
                table: "service_packages",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_packages_hall_id",
                table: "service_packages",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_packages_ServiceId",
                table: "service_packages",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_service_packages_title",
                table: "service_packages",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_services_title",
                table: "services",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_services");

            migrationBuilder.DropTable(
                name: "application_services_templates");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "EmployeeService");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "rented_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "service_packages");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "hall");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropSequence(
                name: "ApplicationServiceTemplateSequence");
        }
    }
}
