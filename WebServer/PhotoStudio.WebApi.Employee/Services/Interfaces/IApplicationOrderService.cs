using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IApplicationOrderService
{
    Task<OrderServiceWithClientDto> UpdateServiceStatus(int orderServiceId, int statusId);
    Task<List<OrderServiceWithClientDto>> GetServicesByEmployee(int employeeId, bool showAll);
    Task<List<OrderServiceWithClientDto>> GetServicesByOrder(int orderId);
}