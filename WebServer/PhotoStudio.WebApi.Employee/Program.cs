using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("connections.json");

builder.Services.AddControllers();
builder.Services.AddSignalR();
    
builder.Services.AddDbContext<PhotoStudioContext>(conf =>
    conf.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

var app = builder.Build();

app.UseRouting();
app.UseAuthorization().UseAuthentication();
app.MapDefaultControllerRoute();

app.Run();