using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase.Models;

namespace PhotoStudio.DataBase;

public sealed class PhotoStudioContext: DbContext
{
    public PhotoStudioContext(DbContextOptions<PhotoStudioContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(EntityConfigure.AddressConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientConfigure);
        modelBuilder.Entity<Contract>(EntityConfigure.ContractConfigure);
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeConfigure);
        modelBuilder.Entity<Hall>(EntityConfigure.HallConfigure);
        modelBuilder.Entity<Order>(EntityConfigure.OrderConfigure);
        modelBuilder.Entity<RentedItem>(EntityConfigure.RentedItemConfigure);
        modelBuilder.Entity<Role>(EntityConfigure.RoleConfigure);
        modelBuilder.Entity<Service>(EntityConfigure.ServiceConfigure);
        modelBuilder.Entity<Status>(EntityConfigure.StatusConfigure);
        modelBuilder.Entity<ApplicationService>(EntityConfigure.ApplicationServiceConfigure);
        modelBuilder.Entity<RefreshToken>(EntityConfigure.RefreshTokenConfigure);
        modelBuilder.Entity<ServicePackage>(EntityConfigure.ServicePackageConfigure);
        modelBuilder.Entity<ApplicationServiceTemplate>(EntityConfigure.ApplicationServiceTemplateConfigure);
    }

    #region Tables

    public DbSet<Address> Addresses { get; internal set; } = null!;
    public DbSet<ApplicationService> ApplicationServices { get; internal set; } = null!;
    public DbSet<Client> Clients { get; internal set; } = null!;
    public DbSet<Contract> Contracts { get; internal set; } = null!;
    public DbSet<Employee> Employees { get; internal set; } = null!;
    public DbSet<Hall> Halls { get; internal set; } = null!;
    public DbSet<Order> Orders { get; internal set; } = null!;
    public DbSet<RentedItem> RentedItems { get; internal set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Service> Services { get; internal set; } = null!;
    public DbSet<Status> Statuses { get; internal set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; internal set; } = null!;
    public DbSet<ServicePackage> ServicePackages { get; internal set; } = null!;
    public DbSet<ApplicationServiceTemplate> ApplicationServiceTemplates { get; internal set; } = null;

    #endregion
}