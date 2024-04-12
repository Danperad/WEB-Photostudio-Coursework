﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Configs;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class ClientService(
    PhotoStudioContext context,
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
        var clients = await context.Clients.Where(c => c.EMail == client1.EMail || c.Phone == client1.Phone).ToListAsync();
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

        var services = new List<ApplicationService>(cart.ServiceModels.Count);
        if (cart.ServicePackage is not null)
        {
            await AddServicesFromPackageAsync(cart.ServicePackage, services);
        }

        var status = await context.Statuses.SingleAsync(s => s.Id == 1);
        foreach (var cartServiceModel in cart.ServiceModels)
        {
            var service = await context.Services.SingleAsync(s => s.Id == cartServiceModel.ServiceId);
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
                    var hall = await context.Halls.FirstAsync(h => h.Id == cartServiceModel.HallId);
                    var newService = new ApplicationService(service, cartServiceModel.StartDateTime!.Value,
                        cartServiceModel.Duration!.Value, hall, status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Photo:
                {
                    CheckServiceModelPresent(cartServiceModel);
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var hall = await context.Halls.FirstAsync(h => h.Id == cartServiceModel.HallId);
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
                    var item = await context.RentedItems
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
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == cartServiceModel.EmployeeId);
                    var hall = await context.Halls.FirstAsync(h => h.Id == cartServiceModel.HallId);
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
        var servicePackage = await context.ServicePackages.Include(p => p.Services)
            .ThenInclude(s => s.Service).Include(servicePackage => servicePackage.Photograph)
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