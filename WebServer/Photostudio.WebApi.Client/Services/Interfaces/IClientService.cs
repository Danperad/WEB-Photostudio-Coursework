using System.Security.Claims;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IClientService
{
    Task<AuthAnswerDto?> AuthClientAsync(AuthModel authModel);
    Task<AuthAnswerDto> RegisterClientAsync(RegisterDto registerDto);
    Task<AuthAnswerDto?> ReAuthClientAsync(string token);
    Task<PhotoStudio.DataBase.Models.Client> AuthClientInContextAsync(ClaimsPrincipal user);
    Task<ClientDto> UpdateClientAsync(ClientDto clientDto, PhotoStudio.DataBase.Models.Client client);

    Task<bool> AddOrderAsync(NewOrderModel cart, PhotoStudio.DataBase.Models.Client client);
}