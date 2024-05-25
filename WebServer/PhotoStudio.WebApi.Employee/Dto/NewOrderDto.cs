﻿namespace PhotoStudio.WebApi.Employee.Dto;

public class NewOrderDto
{
    public int Client { get; set; }
    public NewOrderServicePackageDto? ServicePackage { get; set; }
    public List<NewOrderServiceDto> Services { get; set; }
}