namespace PhotoStudio.WebApi.Lib.Dto;

public class EmployeeDto
{
    public EmployeeDto()
    {
        LastName = "";
        FirstName = "";
    }
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public decimal Cost { get; set; }
}