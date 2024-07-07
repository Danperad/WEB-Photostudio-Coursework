using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Configs;
using PhotoStudio.WebApi.Client.Dto;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class ClientService(
    PhotoStudioContext context,
    IRabbitMqService rabbitMqService,
    IMapper mapper) : IClientService
{
    public async Task<AuthAnswerDto?> AuthClientAsync(AuthModel authModel)
    {
        var client = await context.Clients
            .FirstOrDefaultAsync(c => c.EMail == authModel.Login && c.Password == authModel.Password);
        if (client is null)
        {
            return null;
        }

        var tokens = await GenerateTokenAsync(client);
        return new AuthAnswerDto(tokens.Item1, tokens.Item2, mapper.Map<ClientDto>(client));
    }

    public async Task<AuthAnswerDto> RegisterClientAsync(RegisterDto registerDto)
    {
        var client = registerDto.MapToClient();
        var client1 = client;
        var clients = await context.Clients.Where(c => c.EMail == client1.EMail || c.Phone == client1.Phone)
            .ToListAsync();
        if (clients.Count != 0)
        {
            throw new NotImplementedException(clients.Any(c => c.EMail == client.EMail)
                ? 402.ToString()
                : 400.ToString());
        }

        var newClient = await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
        var tokens = await GenerateTokenAsync(newClient.Entity);
        return new AuthAnswerDto(tokens.Item1, tokens.Item2, mapper.Map<ClientDto>(newClient.Entity));
    }

    public async Task<AuthAnswerDto?> ReAuthClientAsync(string token)
    {
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        var entryToken = context.RefreshTokens.AsNoTracking()
            .Include(t => t.Client)
            .First(t => t.Token == token);
        await context.RefreshTokens.Where(t => t.Token == token).ExecuteDeleteAsync();
        var tokens = await GenerateTokenAsync(entryToken.Client);
        return new AuthAnswerDto(tokens.Item1, tokens.Item2, mapper.Map<ClientDto>(entryToken.Client));
    }

    public async Task<PhotoStudio.DataBase.Models.Client> AuthClientInContextAsync(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.First(c => c.Type == "user");
        var userId = Convert.ToInt32(userIdClaim.Value);
        var client = await context.Clients.FirstAsync(c => c.Id == userId);
        return client;
    }

    public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto, PhotoStudio.DataBase.Models.Client client)
    {
        MapUpdates(clientDto, client);
        var contextClient = context.Entry(client);
        contextClient.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return mapper.Map<ClientDto>(contextClient.Entity);
    }

    public async Task<bool> AddOrderAsync(NewOrderModel cart, PhotoStudio.DataBase.Models.Client client)
    {
        if (cart.ServicePackage is null && cart.ServiceModels.Count == 0)
        {
            throw new NotImplementedException();
        }

        foreach (var service in cart.ServiceModels)
        {
            if (service.StartDateTime.HasValue)
            {
                service.StartDateTime = service.StartDateTime.Value.Date +
                                        TimeSpan.FromMinutes(service.StartDateTime.Value.Minute +
                                                             service.StartDateTime.Value.Hour * 60);
            }
        }

        var services = new List<ApplicationService>(cart.ServiceModels.Count);
        ApplicationService? hallService = null;
        ServicePackage? servicePackage = null;
        if (cart.ServicePackage is not null)
        {
            servicePackage = await context.ServicePackages.Include(p => p.Services)
                .ThenInclude(s => s.Service).Include(sp => sp.Photograph)
                .FirstAsync(s => s.Id == cart.ServicePackage.Id);
            foreach (var service in servicePackage.Services)
            {
                switch (service.Service.Type)
                {
                    case Service.ServiceType.Simple:
                    {
                        var employee = await context.Employees.Where(e => e.BoundServices.Contains(service.Service))
                            .OrderBy(_ => Guid.NewGuid()).FirstAsync();
                        var newService = service.MapToApplicationService(employee);
                        services.Add(newService);
                    }
                        break;
                    case Service.ServiceType.HallRent or Service.ServiceType.ItemRent:
                    {
                        var employee = await context.Employees.Where(e => e.BoundServices.Contains(service.Service))
                            .OrderBy(_ => Guid.NewGuid()).FirstAsync();
                        var newService =
                            service.MapToApplicationService(employee, new DateTime(cart.ServicePackage.StartTime));
                        services.Add(newService);
                        if (service.Service.Type == Service.ServiceType.HallRent)
                            hallService = newService;
                    }
                        break;
                    case Service.ServiceType.Style or Service.ServiceType.Photo:
                    {
                        var employee = servicePackage.Photograph;
                        var newService = service.MapToApplicationService(employee, new DateTime(cart.ServicePackage.StartTime));
                        services.Add(newService);
                    }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        var status =
            await context.Statuses.SingleAsync(s => s.Id == StatusValue.NotAccepted && s.Type == StatusType.Service);
        foreach (var cartServiceModel in cart.ServiceModels.OrderBy(s => s.ServiceId))
        {
            var service = await context.Services.SingleAsync(s => s.Id == cartServiceModel.ServiceId);
            if (services.Any(s => s.Service == service) &&
                service.Type is Service.ServiceType.HallRent or Service.ServiceType.Photo)
                throw new NotImplementedException();
            switch (service.Type)
            {
                case Service.ServiceType.Simple:
                {
                    var newService = new ApplicationService(service, status)
                    {
                        Employee = context.Employees.Where(e => e.BoundServices.Contains(service))
                            .OrderBy(_ => Guid.NewGuid()).First()
                    };
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.HallRent:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    var hall = await context.Halls.FirstAsync(h => h.Id == cartServiceModel.HallId);
                    var newService = new ApplicationService(service, cartServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(cartServiceModel.Duration!.Value), hall, status)
                    {
                        Employee = context.Employees.Where(e => e.BoundServices.Contains(service))
                            .OrderBy(_ => Guid.NewGuid()).First()
                    };
                    hallService = newService;
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Photo:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var newService = new ApplicationService(service, employee, cartServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(cartServiceModel.Duration!.Value), status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.ItemRent:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    if (!cartServiceModel.Number.HasValue)
                        throw new NotImplementedException();
                    var item = await context.RentedItems
                        .FirstAsync(i => i.Id == cartServiceModel.RentedItemId);
                    var newService = new ApplicationService(service, cartServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(cartServiceModel.Duration!.Value), cartServiceModel.Number.Value, item,
                        status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Style:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    if (!cartServiceModel.IsFullTime.HasValue)
                        throw new NotImplementedException();
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var newService = new ApplicationService(service, employee, cartServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(cartServiceModel.Duration!.Value), cartServiceModel.IsFullTime.Value,
                        status);
                    services.Add(newService);
                }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        
        if (hallService is null &&
            services.Any(s => s.Service.Type is Service.ServiceType.Photo or Service.ServiceType.Style))
        {
            throw new NotImplementedException();
        }
        
        foreach (var applicationService in services.Where(s =>
                     s.Service.Type is Service.ServiceType.Photo or Service.ServiceType.Style))
        {
            applicationService.Hall = hallService!.Hall;
        }
        
        var orderStatus =
            await context.Statuses.SingleAsync(s => s.Id == StatusValue.NotAccepted && s.Type == StatusType.Order);
        
        var order = new Order(client, DateTime.Now, services, orderStatus, servicePackage);
        
        var newAddedOrder = await context.Orders.AddAsync(order);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
        rabbitMqService.SendMessage($"New_Order {newAddedOrder.Entity.Id}");
        return true;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByClient(DataBase.Models.Client client)
    {
        var orders = context.Orders.Where(c => c.Client.Id == client.Id).ProjectTo<OrderDto>(mapper.ConfigurationProvider);
        return await orders.ToListAsync();
    }

    private static void CheckServiceModelPresent(NewServiceModel serviceModel)
    {
        if (!serviceModel.StartDateTime.HasValue || !serviceModel.Duration.HasValue)
            throw new NotImplementedException();
    }

    private void MapUpdates(ClientDto clientDto, PhotoStudio.DataBase.Models.Client client)
    {
        if (!string.IsNullOrWhiteSpace(clientDto.Avatar) && client.Avatar != clientDto.Avatar)
            client.Avatar = clientDto.Avatar;
        if (!string.IsNullOrWhiteSpace(clientDto.FirstName) && client.FirstName != clientDto.FirstName)
            client.FirstName = clientDto.FirstName;
        if (!string.IsNullOrWhiteSpace(clientDto.LastName) && client.LastName != clientDto.LastName)
            client.LastName = clientDto.LastName;
        if (!string.IsNullOrWhiteSpace(clientDto.MiddleName) && client.MiddleName != clientDto.MiddleName)
            client.MiddleName = clientDto.MiddleName;
        if (!string.IsNullOrWhiteSpace(clientDto.EMail) && client.EMail != clientDto.EMail &&
            !context.Clients.Any(c => c.EMail == clientDto.EMail)) client.EMail = clientDto.EMail;
        if (!string.IsNullOrWhiteSpace(clientDto.Phone) && client.Phone != clientDto.Phone &&
            !context.Clients.Any(c => c.Phone == clientDto.Phone)) client.Phone = clientDto.Phone;
    }

    private static JwtSecurityToken GenToken(int id, int expiresDays)
    {
        return new JwtSecurityToken(issuer: SecurityOptions.Issuer, audience: SecurityOptions.Audience,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expiresDays)),
            claims: new[] { new Claim("user", id.ToString()) },
            signingCredentials: SecurityOptions.SigningCredentials);
    }

    private async Task<(string, string)> GenerateTokenAsync(PhotoStudio.DataBase.Models.Client client)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.WriteToken(GenToken(client.Id, 1));
        var refreshjwt = handler.WriteToken(GenToken(client.Id, 30));
        var token = new RefreshToken(refreshjwt, client, 30);
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        token = (await context.RefreshTokens.AddAsync(token)).Entity;
        await context.SaveChangesAsync();
        return (jwt, token.Token);
    }
}