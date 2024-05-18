﻿using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<SimpleServiceDto>> GetServices();
}