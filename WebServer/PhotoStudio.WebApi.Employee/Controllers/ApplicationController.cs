using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationController(IApplicationOrderService applicationOrderService) : ControllerBase
{
    [HttpGet("order/{orderId:int}")]
    public IActionResult GetServicesByOrderId(int orderId)
    {
        var ordersServices = applicationOrderService.GetServicesByOrder(orderId);
        return Ok(ordersServices);
    }
    
    [HttpGet]
    public IActionResult GetOrderServices(int employeeId, bool? showAll = false)
    {
        var ordersServices = applicationOrderService.GetServicesByEmployee(employeeId, showAll ?? false);
        return Ok(ordersServices);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateServiceStatus(OrderServicePatchDto orderServicePatchDto)
    {
        var orderService = await applicationOrderService.UpdateServiceStatus(orderServicePatchDto.OrderServiceId, orderServicePatchDto.StatusId);
        return Ok(orderService);
    }
    
}