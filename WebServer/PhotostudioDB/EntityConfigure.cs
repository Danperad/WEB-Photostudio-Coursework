using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotostudioDB.Models;

namespace PhotostudioDB;

internal static class EntityConfigure
{
    internal static void AddressConfigure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Block).HasColumnName("block");
        builder.Property(a => a.Street).HasMaxLength(50).HasColumnName("street");
        builder.Property(a => a.HouseNumber).HasColumnName("house_number");
        builder.Property(a => a.ApartmentNumber).HasColumnName("apartment_number");
        builder.Property(a => a.Settlement).HasColumnName("settlement");
        builder.Property(a => a.CityDistrict).HasColumnName("city_district");
    }

    internal static void ClientConfigure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.LastName).HasMaxLength(50).HasColumnName("last_name");
        builder.Property(c => c.FirstName).HasMaxLength(50).HasColumnName("first_name");
        builder.Property(c => c.MiddleName).HasMaxLength(50).HasColumnName("middle_name");
        builder.Property(c => c.EMail).HasMaxLength(50).HasColumnName("email");
        builder.Property(c => c.Phone).HasMaxLength(16).HasColumnName("phone");
        builder.HasIndex(c => c.Phone).IsUnique();
        builder.Property(c => c.IsActive).HasDefaultValue(true).HasColumnName("is_active");
        builder.Property(c => c.Password).HasMaxLength(64).HasColumnName("password");
        builder.Property(c => c.Avatar).HasColumnName("avatar");
    }

    internal static void ContractConfigure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts");
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.ClientId).HasColumnName("client_id");
        builder.Property(c => c.EmployeeId).HasColumnName("employee_id");
        builder.Property(c => c.StartDate).HasColumnName("start_date");
        builder.Property(c => c.EndDate).HasColumnName("end_date");
        builder.Property(c => c.OrderId).HasColumnName("order_id");
    }

    internal static void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
        builder.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
        builder.Property(e => e.MiddleName).HasMaxLength(50).HasColumnName("middle_name");
        builder.Property(e => e.EMail).HasMaxLength(50).HasColumnName("email");
        builder.Property(e => e.Phone).HasMaxLength(16).HasColumnName("phone");
        builder.HasIndex(e => e.Phone).IsUnique();
        builder.Property(e => e.Passport).HasMaxLength(10).HasColumnName("passport");
        builder.HasIndex(e => e.Passport).IsUnique();
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.RoleId).HasColumnName("role_id");
        builder.Property(e => e.Price).HasColumnName("price").HasColumnType("money");
        builder.Property(c => c.Password).HasMaxLength(64).HasColumnName("password");
    }

    internal static void HallConfigure(EntityTypeBuilder<Hall> builder)
    {
        builder.ToTable("hall");
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.Title).HasMaxLength(50).HasColumnName("title");
        builder.HasIndex(h => h.Title).IsUnique();
        builder.Property(h => h.Description).HasColumnName("description");
        builder.Property(h => h.AddressId).HasColumnName("address_id");
        builder.Property(h => h.PricePerHour).HasColumnName("price_per_hour").HasColumnType("money");
        builder.Property(h => h.Photos).HasColumnName("photos");
    }

    internal static void OrderConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.Property(o => o.Id).HasColumnName("id");
        builder.Property(o => o.ClientId).HasColumnName("client_id");
        builder.Property(o => o.DateTime).HasColumnName("date_time");
        builder.Property(o => o.StatusId).HasColumnName("status_id");
        builder.Property(o => o.ContractId).HasColumnName("contract_id");
        builder.Property(o => o.ServicePackageId).HasColumnName("service_package_id");
        builder.HasOne(o => o.Contract).WithOne(c => c.Order).HasForeignKey<Contract>(c => c.OrderId);
    }

    internal static void RentedItemConfigure(EntityTypeBuilder<RentedItem> builder)
    {
        builder.ToTable("rented_items");
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Title).HasMaxLength(50).HasColumnName("title");
        builder.HasIndex(r => r.Title).IsUnique();
        builder.Property(r => r.Cost).IsRequired().HasColumnType("money").HasColumnName("cost");
        builder.Property(r => r.IsСlothes).HasDefaultValue(false).HasColumnName("is_clothes");
        builder.Property(r => r.IsKids).HasDefaultValue(false).HasColumnName("is_kids");
        builder.Property(r => r.Description).HasColumnName("description");
        builder.Property(r => r.Number).HasColumnName("number");
    }

    internal static void RoleConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(c => c.Title).HasMaxLength(50).HasColumnName("title");
        builder.HasIndex(c => c.Title).IsUnique();
        builder.Property(r => r.Description).HasColumnName("description");
    }

    internal static void ServiceConfigure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.Title).HasMaxLength(50).HasColumnName("title");
        builder.HasIndex(s => s.Title).IsUnique();
        builder.Property(s => s.Cost).HasColumnType("money").HasColumnName("cost");
        builder.Property(s => s.Description).HasColumnName("description");
        builder.Property(s => s.Type).HasColumnName("type");
        builder.Property(s => s.Photos).HasColumnName("photos");
        builder.Property(s => s.Rating).HasColumnName("rating");
    }

    internal static void StatusConfigure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("statuses");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.Title).HasMaxLength(50).HasColumnName("title");
        builder.Property(s => s.Type).HasColumnName("type");
        builder.Property(s => s.Description).HasColumnName("description");
    }

    internal static void ApplicationServiceConfigure(EntityTypeBuilder<ApplicationService> builder)
    {
        builder.ToTable("application_services");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.OrderId).HasColumnName("order_id");
        builder.Property(a => a.ServiceId).HasColumnName("service_id");
        builder.Property(a => a.EmployeeId).HasColumnName("employee_id");
        builder.Property(a => a.StatusId).HasColumnName("status_id");
        builder.Property(a => a.HallId).HasColumnName("hall_id");
        builder.Property(a => a.StartDateTime).HasColumnName("start_date_time");
        builder.Property(a => a.Duration).HasColumnName("duration");
        builder.Property(a => a.AddressId).HasColumnName("address_id");
        builder.Property(a => a.IsFullTime).HasColumnName("is_full_time");
        builder.Property(a => a.RentedItemId).HasColumnName("rented_item_id");
        builder.Property(a => a.Number).HasColumnName("number");
    }

    internal static void ServicePackageConfigure(EntityTypeBuilder<ServicePackage> builder)
    {
        builder.ToTable("service_packages");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.Title).HasMaxLength(50).HasColumnName("title");
        builder.HasIndex(s => s.Title).IsUnique();
        builder.Property(s => s.Description).HasColumnName("description");
        builder.Property(s => s.Photos).HasColumnName("photos");
        builder.Property(s => s.AddressId).HasColumnName("address_id");
        builder.Property(s => s.HallId).HasColumnName("hall_id");
        builder.Property(s => s.EmployeeId).HasColumnName("employee_id");
        builder.Property(s => s.Duration).HasColumnName("duration");
        builder.HasMany(s => s.Services).WithMany(s => s.ServicePackages)
            .UsingEntity(j => j.ToTable("service_package_to_service"));
        builder.Property(s => s.Price).HasColumnName("price").HasColumnType("money");
        builder.Property(s => s.Rating).HasColumnName("rating");
    }

    internal static void RefreshTokenConfigure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(r => r.Token);
        builder.Property(r => r.ClientId).HasColumnName("client_id");
        builder.Property(r => r.Token).HasColumnName("token");
        builder.Property(r => r.SignDate).HasColumnName("sign_date");
    }
}