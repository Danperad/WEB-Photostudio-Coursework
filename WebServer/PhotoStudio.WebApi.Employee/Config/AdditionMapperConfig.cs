using AutoMapper;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Config;

public class AdditionMapperConfig : Profile
{
    public AdditionMapperConfig()
    {
        CreateMap<RentedItem, RentedItemDto>();
        CreateMap<Order, OrderDto>().ForMember(o => o.Number, od => od.MapFrom(src => src.Id));
        CreateMap<ApplicationService, OrderServiceDto>().ForMember(o => o.Duration, od => od.MapFrom(src =>
            TimeSpanToMinutes(src.Duration)));
        CreateMap<ApplicationService, OrderServiceWithClientDto>().ForMember(o => o.Duration, od => od.MapFrom(src =>
            TimeSpanToMinutes(src.Duration))).ForMember(o => o.Client, od => od.MapFrom(src =>
            src.Order.Client)).ForMember(o => o.OrderStatus, od => od.MapFrom(src =>
            src.Order.StatusId));
        CreateMap<Status, StatusDto>();
        CreateMap<Service, SimpleServiceDto>();
    }

    private static int? TimeSpanToMinutes(TimeSpan? span) => (int?)span?.TotalMinutes;
}