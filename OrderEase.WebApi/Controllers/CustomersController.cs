using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Customers;
using OrderEase.Service.Services.Customers.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers;

public class CustomersController(ICustomerService customerService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(CustomerCreateModel model)
    {
        await customerService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 201,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CustomerUpdateModel model)
    {
        await customerService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await customerService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var customer = await customerService.GetAsync(id);

        return Ok(new Response<CustomerViewModel>
        {
            Status = 200,
            Message = "success",
            Data = customer
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string name = null, string phone = null, string email = null)
    {
        var customers = await customerService.GetAllAsync(name,phone,email);

        return Ok(new Response<List<CustomerViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = customers
        });
    }
}
