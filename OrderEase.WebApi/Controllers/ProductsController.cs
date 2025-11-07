using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Services.Products;
using OrderEase.Service.Services.Products.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers;

public class ProductsController(IProductService productService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(ProductCreateModel model)
    {
        await productService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 201,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(long id, [FromBody] ProductUpdateModel model)
    {
        await productService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await productService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        var product = await productService.GetAsync(id);

        return Ok(new Response<ProductViewModel>
        {
            Status = 200,
            Message = "success",
            Data = product
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await productService.GetAllAsync();

        return Ok(new Response<IEnumerable<ProductViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = products
        });
    }
}

