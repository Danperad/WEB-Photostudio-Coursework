using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services;
using WebServer.ASP.Services.Interfaces;
using WebServer.ASP.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(o =>
    o.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()));

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<ApplicationContext>(conf => conf.UseNpgsql(connectionString));

builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IHallRepository, HallRepository>();
builder.Services.AddTransient<IPackageRepository, PackageRepository>();
builder.Services.AddTransient<IRentedItemRepository, RentedItemRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();


builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IHallService, HallService>();
builder.Services.AddTransient<IPackageService, PackageService>();
builder.Services.AddTransient<IRentedItemService, RentedItemService>();
builder.Services.AddTransient<IServiceService, ServiceService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Client, ClientDto>();
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = SecurityOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = SecurityOptions.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = SecurityOptions.GetSymmetricSecurityKey,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

app.UseEndpoints(e => e.MapControllers());
app.UseCors();
app.UseRouting();
app.UseAuthorization().UseAuthentication();

app.Run();