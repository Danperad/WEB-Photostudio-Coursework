using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationController(IApplicationOrderService applicationOrderService) : ControllerBase
{
    [HttpGet("order/{orderId:int}")]
    public async Task<IActionResult> GetServicesByOrderId(int orderId)
    {
        var ordersServices = await applicationOrderService.GetServicesByOrder(orderId);
        return Ok(ordersServices);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrderServices(int employeeId, bool? showAll = false)
    {
        var ordersServices = await applicationOrderService.GetServicesByEmployee(employeeId, showAll ?? false);
        return Ok(ordersServices);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateServiceStatus(OrderServicePatchDto orderServicePatchDto)
    {
        var orderService = await applicationOrderService.UpdateServiceStatus(orderServicePatchDto.OrderServiceId, orderServicePatchDto.StatusId);
        return Ok(orderService);
    }
    
}