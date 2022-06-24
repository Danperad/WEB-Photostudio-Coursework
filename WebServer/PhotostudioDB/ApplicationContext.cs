using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDB.Models;

namespace PhotostudioDB;

public sealed class ApplicationContext : DbContext
{
    private const string ConfigFile = "appsettings.json";

    public ApplicationContext() : this(GetDb())
    {
    }

    internal ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    internal static DbContextOptions<ApplicationContext> GetDb()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
        if (!File.Exists(ConfigFile))
        {
            using var sw = File.Open(ConfigFile, FileMode.Create, FileAccess.Write);
            sw.Write(JsonSerializer.SerializeToUtf8Bytes(new
                {ConnectionStrings = new {DefaultConnection = "Host=;Port=;Database=;Username=;Password="}}));
        }

        builder.AddJsonFile(ConfigFile);
        var config = builder.Build();

        var connectionStrings = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
        return optionsBuilder.UseNpgsql(connectionStrings).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(EntityConfigure.AddressConfigure);
        modelBuilder.Entity<Client>(EntityConfigure.ClientConfigure);
        modelBuilder.Entity<Contract>(EntityConfigure.ContractConfigure);
        modelBuilder.Entity<Employee>(EntityConfigure.EmployeeConfigure);
        modelBuilder.Entity<Hall>(EntityConfigure.HallConfigure);
        modelBuilder.Entity<Order>(EntityConfigure.OrderConfigure);
        modelBuilder.Entity<Profile>(EntityConfigure.ProfileConfigure);
        modelBuilder.Entity<RentedItem>(EntityConfigure.RentedItemConfigure);
        modelBuilder.Entity<Role>(EntityConfigure.RoleConfigure);
        modelBuilder.Entity<Service>(EntityConfigure.ServiceConfigure);
        modelBuilder.Entity<Status>(EntityConfigure.StatusConfigure);
        modelBuilder.Entity<ApplicationService>(EntityConfigure.ApplicationServiceConfigure);
        modelBuilder.Entity<RefreshToken>(EntityConfigure.RefreshTokenConfigure);
        modelBuilder.Entity<ServicePackage>(EntityConfigure.ServicePackageConfigure);
    }

    #region Tables

    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<ApplicationService> ApplicationServices { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Profile> Profiles { get; set; } = null!;
    public DbSet<Hall> Halls { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<RentedItem> RentedItems { get; set; } = null!;
    internal DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<ServicePackage> ServicePackages { get; set; } = null!;

    #endregion
}