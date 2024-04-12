using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Configs;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class ClientService(
    IClientRepository clientRepository,
    IServicePackageRepository servicePackageRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IEmployeeRepository employeeRepository,
    IHallRepository hallRepository,
    IStatusRepository statusRepository,
    IServiceRepository serviceRepository,
    IRentedItemRepository rentedItemRepository,
    IMapper mapper) : IClientService
{
    public async Task<AuthAnswerDto?> AuthClientAsync(AuthModel authModel)
    {
        var client = await clientRepository.GetClients()
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
        client = await clientRepository.AddClientAsync(client);
        var tokens = await GenerateTokenAsync(client);
        return new AuthAnswerDto(tokens.Item1, tokens.Item2, mapper.Map<ClientDto>(client));
    }

    public async Task<AuthAnswerDto?> ReAuthClientAsync(string token)
    {
        var entryToken = (await refreshTokenRepository.GetRefreshTokensAsync()).AsNoTracking()
            .Include(t => t.Client)
            .First(t => t.Token == token);
        await refreshTokenRepository.DeleteRefreshTokenAsync(token);
        var tokens = await GenerateTokenAsync(entryToken.Client);
        return new AuthAnswerDto(tokens.Item1, tokens.Item2, mapper.Map<ClientDto>(entryToken.Client));
    }

    public async Task<PhotoStudio.DataBase.Models.Client> AuthClientInContextAsync(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.First(c => c.Type == "user");
        var userId = Convert.ToInt32(userIdClaim.Value);
        var client = await clientRepository.GetClients().FirstAsync(c => c.Id == userId);
        return client;
    }

    public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto, PhotoStudio.DataBase.Models.Client client)
    {
        MapUpdates(clientDto, client);
        var updatedClient = await clientRepository.UpdateClientAsync(client);
        return mapper.Map<ClientDto>(updatedClient);
    }

    public async Task<bool> AddOrderAsync(NewOrderModel cart, PhotoStudio.DataBase.Models.Client client)
    {
        if (cart.ServicePackage is null && cart.ServiceModels.Count == 0)
        {
            throw new NotImplementedException();
        }

        var services = new List<ApplicationService>(cart.ServiceModels.Count);
        if (cart.ServicePackage is not null)
        {
            await AddServicesFromPackageAsync(cart.ServicePackage, services);
        }

        var status = await statusRepository.GetStatusById(1);
        foreach (var cartServiceModel in cart.ServiceModels)
        {
            var service = await serviceRepository.GetServices().SingleAsync(s => s.Id == cartServiceModel.ServiceId);
            switch (service.Type)
            {
                case Service.ServiceType.Simple:
                {
                    var newService = new ApplicationService(service, status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.HallRent:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    var hall = await hallRepository.GetHalls().FirstAsync(h => h.Id == cartServiceModel.HallId);
                    var newService = new ApplicationService(service, cartServiceModel.StartDateTime!.Value,
                        cartServiceModel.Duration!.Value, hall, status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Photo:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    var employee = await employeeRepository.GetEmployees()
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var hall = await hallRepository.GetHalls().FirstAsync(h => h.Id == cartServiceModel.HallId);
                    var newService = new ApplicationService(service, employee, cartServiceModel.StartDateTime!.Value,
                        cartServiceModel.Duration!.Value, hall, status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.ItemRent:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    if (!cartServiceModel.Number.HasValue)
                        throw new NotImplementedException();
                    var item = await rentedItemRepository.GetItems()
                        .FirstAsync(i => i.Id == cartServiceModel.RentedItemId);
                    var newService = new ApplicationService(service, cartServiceModel.StartDateTime!.Value,
                        cartServiceModel.Duration!.Value, cartServiceModel.Number.Value, item, status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Style:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    if (!cartServiceModel.IsFullTime.HasValue)
                        throw new NotImplementedException();
                    var employee = await employeeRepository.GetEmployees()
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var hall = await hallRepository.GetHalls().FirstAsync(h => h.Id == cartServiceModel.HallId);
                    var newService = new ApplicationService(service, employee, cartServiceModel.StartDateTime!.Value,
                        cartServiceModel.Duration!.Value, hall, cartServiceModel.IsFullTime.Value, status);
                    services.Add(newService);
                }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return true;
    }

    private async Task AddServicesFromPackageAsync(NewServicePackageModel packageModel,
        List<ApplicationService> services)
    {
        var servicePackage = await servicePackageRepository.GetPackages().Include(p => p.Services)
            .ThenInclude(s => s.Service)
            .FirstAsync(s => s.Id == packageModel.Id);
        foreach (var service in servicePackage.Services)
        {
            switch (service.Service.Type)
            {
                case Service.ServiceType.Simple:
                {
                    var newService = service.MapToApplicationService();
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.HallRent:
                case Service.ServiceType.ItemRent:
                case Service.ServiceType.Style:
                {
                    var newService =
                        service.MapToApplicationService(startDateTime: new DateTime(packageModel.StartTime));
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Photo:
                {
                    var employee = servicePackage.Photograph;
                    var newService = service.MapToApplicationService(employee, new DateTime(packageModel.StartTime));
                    services.Add(newService);
                }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    private static void CheckServiceModelPresent(NewServiceModel serviceModel)
    {
        if (!serviceModel.StartDateTime.HasValue || serviceModel.Duration.HasValue)
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
            !clientRepository.GetClients().Any(c => c.EMail == clientDto.EMail)) client.EMail = clientDto.EMail;
        if (!string.IsNullOrWhiteSpace(clientDto.Phone) && client.Phone != clientDto.Phone &&
            !clientRepository.GetClients().Any(c => c.Phone == clientDto.Phone)) client.Phone = clientDto.Phone;
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
        token = await refreshTokenRepository.AddRefreshTokenAsync(token);
        return (jwt, token.Token);
    }
}