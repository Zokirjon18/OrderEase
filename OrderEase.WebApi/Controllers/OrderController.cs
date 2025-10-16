using Microsoft.AspNetCore.Mvc;
using OrderEase.Domain.Enums;
using OrderEase.Service.Services.Orders;
using OrderEase.Service.Services.Orders.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers;

public class OrderController(IOrderService orderService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(OrderCreateModel model)
    {
        await orderService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 201,
            Message = "success",
        });
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await orderService.CancelAsync(id);

        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        var product = await orderService.GetAsync(id);

        return Ok(new Response<OrderViewModel>
        {
            Status = 200,
            Message = "success",
            Data = product
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(long? customerId = null, OrderStatus? status = null)
    {
        var products = await orderService.GetAllAsync(customerId, status);

        return Ok(new Response<IEnumerable<OrderViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = products
        });
    }
}

