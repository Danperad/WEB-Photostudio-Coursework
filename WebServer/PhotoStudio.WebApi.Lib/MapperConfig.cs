using AutoMapper;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Lib;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Employee, EmployeeDto>().ForMember(e => e.Cost, ed => ed.MapFrom(src => src.Price));
        CreateMap<Hall, HallDto>();
        CreateMap<ServicePackage, ServicePackageDto>();
        CreateMap<RentedItem, RentedItemDto>();
        CreateMap<Service, ServiceDto>();
    }
}