using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Dto;

public class EmployeeWithRoleDto : EmployeeDto
{
    public EmployeeWithRoleDto()
    {
        Role = new RoleDto();
        BoundServices = new List<SimpleServiceDto>();
    }
    public RoleDto Role { get; set; } 
    public IReadOnlyList<SimpleServiceDto> BoundServices { get; set; }
}