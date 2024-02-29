using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Lib.Dto;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Cost { get; set; }

    public static EmployeeDto GetModel(Employee item)
    {
        return new EmployeeDto {Id = item.Id, Cost = item.Price!.Value, Title = item.FullName};
    }
}