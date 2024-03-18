using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class ClientService(PhotoStudioContext context, IMapper mapper) : IClientService
{
    public async Task<IEnumerable<ClientDto>> GetClients(string? search, int count, int start)
    {
        IQueryable<Client> clients = context.Clients.AsNoTracking().OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search}%";
            clients = clients.Where(c =>
                EF.Functions.ILike(c.LastName, pattern) || EF.Functions.ILike(c.FirstName, pattern) ||
                (c.MiddleName != null && EF.Functions.ILike(c.MiddleName, pattern)));
        }

        clients = clients.Skip(start).Take(count);
        var res = await clients.ToListAsync();
        return mapper.Map<List<Client>, List<ClientDto>>(res);
    }
}