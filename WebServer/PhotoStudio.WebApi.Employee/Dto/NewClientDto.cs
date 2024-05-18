namespace PhotoStudio.WebApi.Employee.Dto;

public record NewClientDto(string LastName, string FirstName, string? MiddleName, string Phone, string EMail);