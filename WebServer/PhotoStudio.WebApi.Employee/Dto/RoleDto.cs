namespace PhotoStudio.WebApi.Employee.Dto;

public class RoleDto
{
    public RoleDto()
    {
        Title = "";
    }
    public int Id { get; set; }
    public string Title { get; set; }
}