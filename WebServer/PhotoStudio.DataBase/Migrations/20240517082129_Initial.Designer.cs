﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhotoStudio.DataBase;

#nullable disable

namespace PhotoStudio.DataBase.Migrations
{
    [DbContext(typeof(PhotoStudioContext))]
    [Migration("20240517082129_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("ApplicationServiceTemplateSequence");

            modelBuilder.Entity("EmployeeService", b =>
                {
                    b.Property<int>("BoundEmployeesId")
                        .HasColumnType("integer");

                    b.Property<int>("BoundServicesId")
                        .HasColumnType("integer");

                    b.HasKey("BoundEmployeesId", "BoundServicesId");

                    b.HasIndex("BoundServicesId");

                    b.ToTable("EmployeeService");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApartmentNumber")
                        .HasColumnType("text")
                        .HasColumnName("apartment_number");

                    b.Property<string>("Block")
                        .HasColumnType("text")
                        .HasColumnName("block");

                    b.Property<string>("CityDistrict")
                        .HasColumnType("text")
                        .HasColumnName("city_district");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("house_number");

                    b.Property<string>("Settlement")
                        .HasColumnType("text")
                        .HasColumnName("settlement");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("street");

                    b.HasKey("Id");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ApplicationService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BingingPackageId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("interval")
                        .HasColumnName("duration");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<int?>("HallId")
                        .HasColumnType("integer")
                        .HasColumnName("hall_id");

                    b.Property<bool?>("IsFullTime")
                        .HasColumnType("boolean")
                        .HasColumnName("is_full_time");

                    b.Property<int?>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int?>("RentedItemId")
                        .HasColumnType("integer")
                        .HasColumnName("rented_item_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date_time");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<int>("StatusType")
                        .HasColumnType("integer")
                        .HasColumnName("status_type");

                    b.HasKey("Id");

                    b.HasIndex("BingingPackageId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("HallId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RentedItemId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StatusId", "StatusType");

                    b.ToTable("application_services", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ApplicationServiceTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('\"ApplicationServiceTemplateSequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("interval")
                        .HasColumnName("duration");

                    b.Property<int?>("HallId")
                        .HasColumnType("integer")
                        .HasColumnName("hall_id");

                    b.Property<bool?>("IsFullTime")
                        .HasColumnType("boolean")
                        .HasColumnName("is_full_time");

                    b.Property<int?>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int?>("RentedItemId")
                        .HasColumnType("integer")
                        .HasColumnName("rented_item_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<int>("ServicePackageId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<int>("StatusType")
                        .HasColumnType("integer")
                        .HasColumnName("status_type");

                    b.Property<int?>("StylistId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("RentedItemId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ServicePackageId");

                    b.HasIndex("StylistId");

                    b.HasIndex("StatusId", "StatusType");

                    b.ToTable("application_services_templates", (string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("middle_name");

                    b.Property<string>("Password")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("contracts", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("middle_name");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("passport");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("phone");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("Passport")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer")
                        .HasColumnName("address_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<List<string>>("Photos")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("photos");

                    b.Property<decimal>("PricePerHour")
                        .HasColumnType("money")
                        .HasColumnName("price_per_hour");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("hall", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<int>("ContractId")
                        .HasColumnType("integer")
                        .HasColumnName("contract_id");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_time");

                    b.Property<int?>("ServicePackageId")
                        .HasColumnType("integer")
                        .HasColumnName("service_package_id");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<int>("StatusType")
                        .HasColumnType("integer")
                        .HasColumnName("status_type");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServicePackageId");

                    b.HasIndex("StatusId", "StatusType");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("SignDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("sign_date");

                    b.HasKey("Token");

                    b.HasIndex("ClientId");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.RentedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("money")
                        .HasColumnName("cost");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long>("Number")
                        .HasColumnType("bigint")
                        .HasColumnName("number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("rented_items", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("money")
                        .HasColumnName("cost");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<List<string>>("Photos")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("photos");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("services", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ServicePackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<int>("HallId")
                        .HasColumnType("integer")
                        .HasColumnName("hall_id");

                    b.Property<List<string>>("Photos")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("photos");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("HallId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("service_packages", (string)null);
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id", "Type");

                    b.ToTable("statuses", (string)null);
                });

            modelBuilder.Entity("EmployeeService", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("BoundEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("BoundServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ApplicationService", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.ServicePackage", "BingingPackage")
                        .WithMany()
                        .HasForeignKey("BingingPackageId");

                    b.HasOne("PhotoStudio.DataBase.Models.Employee", "Employee")
                        .WithMany("Services")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("PhotoStudio.DataBase.Models.Hall", "Hall")
                        .WithMany("Services")
                        .HasForeignKey("HallId");

                    b.HasOne("PhotoStudio.DataBase.Models.Order", "Order")
                        .WithMany("Services")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.RentedItem", "RentedItem")
                        .WithMany("Services")
                        .HasForeignKey("RentedItemId");

                    b.HasOne("PhotoStudio.DataBase.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId", "StatusType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BingingPackage");

                    b.Navigation("Employee");

                    b.Navigation("Hall");

                    b.Navigation("Order");

                    b.Navigation("RentedItem");

                    b.Navigation("Service");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ApplicationServiceTemplate", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Hall", "Hall")
                        .WithMany()
                        .HasForeignKey("HallId");

                    b.HasOne("PhotoStudio.DataBase.Models.RentedItem", "RentedItem")
                        .WithMany()
                        .HasForeignKey("RentedItemId");

                    b.HasOne("PhotoStudio.DataBase.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.ServicePackage", "ServicePackage")
                        .WithMany("Services")
                        .HasForeignKey("ServicePackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Employee", "Stylist")
                        .WithMany()
                        .HasForeignKey("StylistId");

                    b.HasOne("PhotoStudio.DataBase.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId", "StatusType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("RentedItem");

                    b.Navigation("Service");

                    b.Navigation("ServicePackage");

                    b.Navigation("Status");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Contract", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Order", "Order")
                        .WithOne("Contract")
                        .HasForeignKey("PhotoStudio.DataBase.Models.Contract", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Employee", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Hall", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Order", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.ServicePackage", "ServicePackage")
                        .WithMany()
                        .HasForeignKey("ServicePackageId");

                    b.HasOne("PhotoStudio.DataBase.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId", "StatusType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ServicePackage");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.RefreshToken", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ServicePackage", b =>
                {
                    b.HasOne("PhotoStudio.DataBase.Models.Employee", "Photograph")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Hall", "Hall")
                        .WithMany()
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoStudio.DataBase.Models.Service", null)
                        .WithMany("ServicePackages")
                        .HasForeignKey("ServiceId");

                    b.Navigation("Hall");

                    b.Navigation("Photograph");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Client", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Employee", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Hall", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Order", b =>
                {
                    b.Navigation("Contract");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.RentedItem", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.Service", b =>
                {
                    b.Navigation("ServicePackages");
                });

            modelBuilder.Entity("PhotoStudio.DataBase.Models.ServicePackage", b =>
                {
                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
