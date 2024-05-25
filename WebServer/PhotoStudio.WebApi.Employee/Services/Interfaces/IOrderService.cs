using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IOrderService
{
    IAsyncEnumerable<OrderDto> GetAllOrders(int count, int start);
    IAsyncEnumerable<OrderDto> GetOrdersByClient(int clientId);
    Task<OrderDto> AddNewOrder(NewOrderDto order);
    Task<OrderDto> ChangeOrderStatus(int orderId, int statusId);
}