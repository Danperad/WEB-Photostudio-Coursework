using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;
using WebServer.ASP.Utils;

namespace WebServer.ASP.Services;

public class ClientService(
    IClientRepository clientRepository,
    IServicePackageRepository servicePackageRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IEmployeeRepository employeeRepository,
    IHallRepository hallRepository,
    IServiceRepository serviceRepository,
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

    public async Task<Client> AuthClientInContextAsync(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.First(c => c.Type == "user");
        var userId = Convert.ToInt32(userIdClaim.Value);
        var client = await clientRepository.GetClients().FirstAsync(c => c.Id == userId);
        return client;
    }

    public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto, Client client)
    {
        MapUpdates(clientDto, client);
        var updatedClient = await clientRepository.UpdateClientAsync(client);
        return mapper.Map<ClientDto>(updatedClient);
    }

    public async Task<bool> AddOrderAsync(NewOrderModel cart, Client client)
    {
        if (cart.ServicePackage is null && cart.ServiceModels.Count == 0)
        {
            throw new NotImplementedException();
        }

        var services = new List<ApplicationService>(cart.ServiceModels.Count);
        var status = StatusStore.Statuses.First(st => st.Id == 7);
        if (cart.ServicePackage is not null)
        {
            await AddServicesFromPackage(cart.ServicePackage, services);
        }

        foreach (var cartServiceModel in cart.ServiceModels)
        {
            var service = await serviceRepository.GetServices().SingleAsync(s => s.Id == cartServiceModel.ServiceId);
            switch (service.Type)
            {
                case Service.Status.Simple:
                    {
                        var newService = await GetSimpleServiceAsync(service);
                        services.Add(newService);
                    }
                    break;
                case Service.Status.HallRent:
                    break;
                case Service.Status.Photo:
                    break;
                case Service.Status.ItemRent:
                    break;
                case Service.Status.Style:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private async Task AddServicesFromPackage(NewServicePackageModel packageModel, List<ApplicationService> services)
    {
        var servicePackage = servicePackageRepository.GetPackages().Include(p => p.Services)
                .First(s => s.Id == packageModel.Id);
        foreach (var service in servicePackage.Services)
        {
            switch (service.Type)
            {
                case Service.Status.Simple:
                    {
                        var newService = await GetSimpleServiceAsync(service);
                        services.Add(newService);
                    }
                    break;
                case Service.Status.HallRent:
                    {
                        var newService = await GetHallServiceAsync(service, servicePackage.HallId, new DateTime(packageModel.StartTime));
                        services.Add(newService);
                    }
                    break;
                case Service.Status.Photo:
                    {
                        var employee = servicePackage.Photograph;
                        var hall = servicePackage.Hall;
                        var newService = new ApplicationService(service, employee,
                            new DateTime(cart.ServicePackage.StartTime),
                            servicePackage.Duration, hall, status);
                        services.Add(newService);
                    }
                    break;
                case Service.Status.ItemRent:
                    break;
                case Service.Status.Style:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(service.Type));
            }
        }
        return true;
    }

    private async Task<ApplicationService> GetSimpleServiceAsync(Service service)
    {
        var employee = service.Id switch
        {
            3 => employeeRepository.GetEmployees().FirstAsync(e => e.RoleId == 3),
            4 => employeeRepository.GetEmployees().FirstAsync(e => e.RoleId == 5),
            _ => employeeRepository.GetEmployees().FirstAsync(e => e.RoleId == 7)
        };
        return new ApplicationService(service, employee, status);
    }

    private async Task<ApplicationService> GetHallServiceAsync(Service services, int hallId, DateTime startDate){
        var employee = await employeeRepository.GetEmployees().FirstAsync(e => e.RoleId == 7);
        var hall = await hallRepository.GetHalls().FirstAsync(h => h.Id == hallId);
        return new ApplicationService(services, employee, startDate, hall, status);
    }

    private void MapUpdates(ClientDto clientDto, Client client)
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

    private async Task<(string, string)> GenerateTokenAsync(Client client)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.WriteToken(GenToken(client.Id, 1));
        var refreshjwt = handler.WriteToken(GenToken(client.Id, 30));
        var token = new RefreshToken(refreshjwt, client, 30);
        token = await refreshTokenRepository.AddRefreshTokenAsync(token);
        return (jwt, token.Token);
    }
}