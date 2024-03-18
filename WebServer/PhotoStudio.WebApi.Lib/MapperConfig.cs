using AutoMapper;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Lib;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Hall, HallDto>();
        CreateMap<ServicePackage, ServicePackageDto>();
        CreateMap<RentedItem, RentedItemDto>();
        CreateMap<Service, ServiceDto>();
    }
}