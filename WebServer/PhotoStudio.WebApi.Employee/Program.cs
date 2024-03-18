using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Controllers;
using PhotoStudio.WebApi.Employee.Services;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("connections.json");

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddDbContext<PhotoStudioContext>(conf =>
    conf.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddTransient<IClientService, ClientService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials
}

app.UseRouting();
app.UseAuthorization().UseAuthentication();
app.MapDefaultControllerRoute();
app.MapHub<ClientHub>("/clients");

app.Run();