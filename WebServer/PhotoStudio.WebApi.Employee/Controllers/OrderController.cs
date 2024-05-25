using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllOrders(int? number = null, int? start = null)
    {
        var orders = orderService.GetAllOrders(number ?? 20, start ?? 0);
        return Ok(orders);
    }

    [HttpGet("client/{clientId:int}")]
    public IActionResult GetOrdersByClient(int clientId)
    {
        var orders = orderService.GetOrdersByClient(clientId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewOrder(NewOrderDto order)
    {
        var newOrder = await orderService.AddNewOrder(order);
        return Ok(newOrder);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateOrderStatus(OrderPatchDto patchDto)
    {
        var order = await orderService.ChangeOrderStatus(patchDto.OrderId, patchDto.StatusId);
        return Ok(order);
    }
}