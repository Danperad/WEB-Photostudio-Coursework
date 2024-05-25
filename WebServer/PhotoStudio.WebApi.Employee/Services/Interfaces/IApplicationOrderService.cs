using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IApplicationOrderService
{
    Task<OrderServiceWithClientDto> UpdateServiceStatus(int orderServiceId, int statusId);
    IAsyncEnumerable<OrderServiceWithClientDto> GetServicesByEmployee(int employeeId, bool showAll);
    IAsyncEnumerable<OrderServiceWithClientDto> GetServicesByOrder(int orderId);
}