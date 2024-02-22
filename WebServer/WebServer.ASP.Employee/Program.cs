using Microsoft.EntityFrameworkCore;
using PhotostudioDB;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("connections.json");

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(conf =>
    conf.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

var app = builder.Build();

app.UseRouting();
app.UseAuthorization().UseAuthentication();
app.MapDefaultControllerRoute();

app.Run();