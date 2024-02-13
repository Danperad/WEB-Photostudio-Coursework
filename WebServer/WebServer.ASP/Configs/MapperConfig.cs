using AutoMapper;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;

namespace WebServer.ASP.Configs;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Client, ClientDto>();
    }
}