using AutoMapper;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Dto;

namespace PhotoStudio.WebApi.Client.Configs;

public class AdditionMapperConfig : Profile
{
    public AdditionMapperConfig()
    {
        CreateMap<ApplicationService, OrderServiceDto>();
        CreateMap<Order, OrderDto>().ForMember(o => o.Number, od => od.MapFrom(src => src.Id));
        CreateMap<ApplicationService, OrderServiceDto>().ForMember(o => o.Duration, od => od.MapFrom(src =>
            TimeSpanToMinutes(src.Duration))).ForMember(o => o.OrderStatus, od => od.MapFrom(src =>
            src.Order.StatusId));
    }
    private static int? TimeSpanToMinutes(TimeSpan? span) => (int?)span?.TotalMinutes;
}