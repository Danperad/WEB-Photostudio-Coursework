﻿using AutoMapper;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Lib.Dto;
using RentedItemDto = PhotoStudio.WebApi.Employee.Dto.RentedItemDto;

namespace PhotoStudio.WebApi.Employee.Config;

public class AdditionMapperConfig : Profile
{
    public AdditionMapperConfig()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RentedItem, RentedItemDto>();
        CreateMap<Order, OrderDto>().ForMember(o => o.Number, od => od.MapFrom(src => src.Id));
        CreateMap<ApplicationService, OrderServiceWithClientDto>().ForMember(o => o.Duration, od => od.MapFrom(src =>
                TimeSpanToMinutes(src.Duration))).ForMember(o => o.Client, od => od.MapFrom(src =>
                src.Order.Client)).ForMember(o => o.OrderStatus, od => od.MapFrom(src =>
                src.Order.StatusId)).ForMember(o => o.Count, od => od.MapFrom(src => src.Number))
            .ForMember(o => o.Item, od => od.MapFrom(src => src.RentedItem));
        CreateMap<Status, StatusDto>();
        CreateMap<ServicePackage, ServicePackageWithoutPhotosDto>()
            .ForMember(o => o.Cost, od => od.MapFrom(src => src.Price));

        CreateMap<Client, ClientReportDto>();
        CreateMap<ServicePackage, ServicePackageReportDto>();
        CreateMap<DataBase.Models.Employee, EmployeeReportDto>();
        CreateMap<ApplicationService, ApplicationServiceReportDto>()
            .ForMember(a => a.Service, ad => ad.MapFrom(src => src.Service.Title))
            .ForMember(a => a.Hall, ad => ad.MapFrom(src => src.Hall != null ? src.Hall.Title : null))
            .ForMember(a => a.Item, ad => ad.MapFrom(src => src.RentedItem != null ? src.RentedItem.Title : null))
            .ForMember(o => o.Status, od => od.MapFrom(src => src.Status.Title));
        CreateMap<Order, OrderReportDto>().ForMember(o => o.Number, od => od.MapFrom(src => src.Id))
            .ForMember(o => o.Status, od => od.MapFrom(src => src.Status.Title));
        CreateMap<DataBase.Models.Employee, EmployeeWithRoleDto>()
            .ForMember(e => e.Cost, ed => ed.MapFrom(src => src.Price));
    }

    private static int? TimeSpanToMinutes(TimeSpan? span) => (int?)span?.TotalMinutes;
}