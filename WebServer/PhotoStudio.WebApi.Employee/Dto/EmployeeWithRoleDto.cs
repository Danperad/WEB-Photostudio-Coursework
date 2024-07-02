using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Dto;

public class EmployeeWithRoleDto : EmployeeDto
{
    public RoleDto Role { get; set; } 
    public List<SimpleServiceDto> BoundServices { get; set; }
}