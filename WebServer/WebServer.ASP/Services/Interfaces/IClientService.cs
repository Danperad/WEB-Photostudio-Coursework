using System.Security.Claims;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IClientService
{
    AuthAnswerDto? AuthClient(AuthModel authModel);
    Task<AuthAnswerDto?> AuthClientAsync(AuthModel authModel);
    AuthAnswerDto RegisterClient(RegisterDto registerDto);
    Task<AuthAnswerDto> RegisterClientAsync(RegisterDto registerDto);
    AuthAnswerDto? ReAuthClient(string token);
    Task<AuthAnswerDto?> ReAuthClientAsync(string token);
    Client AuthClientInContext(ClaimsPrincipal user);
    Task<Client> AuthClientInContextAsync(ClaimsPrincipal user);
    ClientDto UpdateClient(ClientDto clientDto, Client client);
    Task<ClientDto> UpdateClientAsync(ClientDto clientDto, Client client);

    bool AddOrder(NewOrderModel cart, Client client);
    Task<bool> AddOrderAsync(NewOrderModel cart, Client client);
}