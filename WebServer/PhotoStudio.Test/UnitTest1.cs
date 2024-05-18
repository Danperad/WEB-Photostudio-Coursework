using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Controllers;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

namespace PhotoStudio.Test;

public class Tests
{
    private IServiceService _serviceService;
    private List<HallDto> _hallDtos;

    [SetUp]
    public void Setup()
    {
        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();
        var context = new PhotoStudioContext(new DbContextOptionsBuilder<PhotoStudioContext>()
            .UseSqlite(keepAliveConnection).Options);

        context.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MapperConfig)));
        var mapper = new Mapper(mapperConfig);
        _serviceService = new ServiceService(context, mapper);
        List<Service> services =
        [
            new Service
            {
                Id = 1, Title = "Test1", Description = "Description1", Type = Service.ServiceType.Simple, Cost = 1000
            },
            new Service
            {
                Id = 2, Title = "Test2", Description = "Description2", Type = Service.ServiceType.HallRent, Cost = 2000
            },
            new Service
            {
                Id = 3, Title = "Test3", Description = "Description3", Type = Service.ServiceType.Photo, Cost = 3000
            },
            new Service
            {
                Id = 4, Title = "Test4", Description = "Description4", Type = Service.ServiceType.ItemRent, Cost = 4000
            },
            new Service
            {
                Id = 5, Title = "Test5", Description = "Description5", Type = Service.ServiceType.Style, Cost = 5000
            }
        ];
        context.AddRange(services);
        context.SaveChanges();

        _hallDtos =
        [
            new HallDto(1, "Hall1", "Description1",  5000, new List<string>()),
            new HallDto(2, "Hall2", "Description2",  5000, new List<string>()),
            new HallDto(3, "Hall3", "Description3",  5000, new List<string>()),
        ];
    }

    [TestCase(null, null, null, null, ExpectedResult = 5)]
    [TestCase(3, null, null, null, ExpectedResult = 5)]
    [TestCase(null, 5, null, null, ExpectedResult = 5)]
    [TestCase(5, 1, null, null, ExpectedResult = 5)]
    [TestCase(2, 1, null, null, ExpectedResult = 2)]
    [TestCase(5, 3, null, null, ExpectedResult = 3)]
    [TestCase(null, null, 1, null, ExpectedResult = 1)]
    [TestCase(null, null, null, "Test5", ExpectedResult = 1)]
    [TestCase(null, null, null, "Test", ExpectedResult = 5)]
    public int GetServicesTest(int? count, int? start, int? type, string? search)
    {
        var res = _serviceService.GetAllServicesAsync(count, start, null, type, search).Result;
        return res.Count();
    }

    private static object[] HallCases =
    {
        new object[] { DateTime.Now.AddDays(2), 60, true },
        new object[] { DateTime.Now.AddHours(4), 60, false },
        new object[] { DateTime.Now.AddDays(2), 29, false }
    };

    [TestCaseSource(nameof(HallCases))]
    public void GetFreeHallsTest(DateTime start, int duration, bool resultStatus)
    {
        var mock = new Mock<IHallService>();
        mock.Setup(h => h.GetAvailableHallsAsync(It.IsAny<DateTime>(), It.IsAny<int>())).ReturnsAsync(_hallDtos);
        var controller = new HallController(mock.Object);
        var res = controller.GetFree(start, duration).Result;
        Assert.That(((res as ObjectResult)?.Value as AnswerDto)!.Status, Is.EqualTo(resultStatus));
    }

    [TestCase("login", "password", ExpectedResult = 101)]
    [TestCase("  ", "password", ExpectedResult = 400)]
    [TestCase("login", "   ", ExpectedResult = 400)]
    [TestCase("", "   ", ExpectedResult = 400)]
    public int LoginUserTest(string login, string password)
    {
        var mock = new Mock<IClientService>();
        mock.Setup(h => h.AuthClientAsync(It.IsAny<AuthModel>())).ReturnsAsync((AuthAnswerDto?)null);
        var authController = new AuthController(mock.Object);
        var res = authController.LoginUser(new AuthModel { Login = login, Password = password }).Result;
        return ((res as ObjectResult)?.Value as AnswerDto)!.Error!.Value;
    }
}