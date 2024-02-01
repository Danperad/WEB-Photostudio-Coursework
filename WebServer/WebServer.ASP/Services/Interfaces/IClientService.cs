using System.Security.Claims;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IClientService
{
    Task<AuthAnswerDto?> AuthClientAsync(AuthModel authModel);
    Task<AuthAnswerDto> RegisterClientAsync(RegisterDto registerDto);
    Task<AuthAnswerDto?> ReAuthClientAsync(string token);
    Task<Client> AuthClientInContextAsync(ClaimsPrincipal user);
    Task<ClientDto> UpdateClientAsync(ClientDto clientDto, Client client);

    Task<bool> AddOrderAsync(NewOrderModel cart, Client client);
}