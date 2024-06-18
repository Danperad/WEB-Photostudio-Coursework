using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Config;
using PhotoStudio.WebApi.Employee.Hubs;
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

builder.Services.AddAutoMapper(typeof(MapperConfig), typeof(AdditionMapperConfig));

builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IServicePackageService, ServicePackageService>();
builder.Services.AddTransient<IHallService, HallService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IRentedItemService, RentedItemService>();
builder.Services.AddTransient<IApplicationOrderService, ApplicationOrderService>();
builder.Services.AddTransient<IReportService, ReportService>();

builder.Services.AddHostedService<QueueBackgroundService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(_ => true) // allow any origin
        .AllowCredentials()); // allow credentials
}

app.UseRouting();
app.UseAuthorization().UseAuthentication();
app.MapDefaultControllerRoute();
app.MapHub<MainHub>("/hub");

app.Run();