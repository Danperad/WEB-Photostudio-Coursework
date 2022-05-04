using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotostudioDB.Models;
using PhotostudioDB.Models.Services;

namespace PhotostudioDB;

public sealed class ApplicationContext : DbContext
{
    private const string ConfigFile = "appsettings.json";

    internal ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.Migrate();
    }

    internal static string? ConnectionStrings { get; private set; }

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

        ConnectionStrings = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
        return optionsBuilder.UseNpgsql(ConnectionStrings).Options;
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
        modelBuilder.Entity<HallRentService>(EntityConfigure.HallRentServiceConfigure);
        modelBuilder.Entity<PhotoVideoService>(EntityConfigure.PhotoVideoServiceConfigure);
        modelBuilder.Entity<RentService>(EntityConfigure.RentServiceConfigure);
        modelBuilder.Entity<StyleService>(EntityConfigure.StyleServiceConfigure);
        modelBuilder.Entity<RefreshToken>().HasIndex(a => a.Token).IsUnique();
    }

    public override void Dispose()
    {
        DbWorker.UnLoad();
        base.Dispose();
    }

    #region Tables

    internal DbSet<Address> Addresses { get; set; } = null!;
    internal DbSet<ApplicationService> ApplicationServices { get; set; } = null!;
    internal DbSet<HallRentService> HallRentServices { get; set; } = null!;
    internal DbSet<PhotoVideoService> PhotoVideoServices { get; set; } = null!;
    internal DbSet<RentService> RentServices { get; set; } = null!;
    internal DbSet<StyleService> StyleServices { get; set; } = null!;
    internal DbSet<Client> Clients { get; set; } = null!;
    internal DbSet<Contract> Contracts { get; set; } = null!;
    internal DbSet<Employee> Employees { get; set; } = null!;
    internal DbSet<Profile> Profiles { get; set; } = null!;
    internal DbSet<Hall> Halls { get; set; } = null!;
    internal DbSet<Order> Orders { get; set; } = null!;
    internal DbSet<RentedItem> RentedItems { get; set; } = null!;
    internal DbSet<Role> Roles { get; set; } = null!;
    internal DbSet<Service> Services { get; set; } = null!;
    internal DbSet<Status> Statuses { get; set; } = null!;
    
    internal DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    

    #endregion
}