using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Client.Configs;
using PhotoStudio.WebApi.Client.Services;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("connections.json");

builder.Services.AddControllers();

builder.Services.AddCors(o =>
{
    if (builder.Environment.IsDevelopment())
    {
        o.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"));
    }
    else
    {
        o.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173", "http://*.danperad.ru").AllowCredentials());
    }
});

builder.Services.AddDbContext<PhotoStudioContext>(conf =>
    conf.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Services.AddAutoMapper(typeof(MapperConfig), typeof(AdditionMapperConfig));

builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IHallService, HallService>();
builder.Services.AddTransient<IPackageService, PackageService>();
builder.Services.AddTransient<IRentedItemService, RentedItemService>();
builder.Services.AddTransient<IServiceService, ServiceService>();


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

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization().UseAuthentication();
app.MapDefaultControllerRoute();
app.UseCors();

app.Run();