using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Products;
using OrderEase.Service.Services.Products.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductCreateModel model)
        {
            try
            {
                await productService.CreateAsync(model);

                return Ok(new Response
                {
                    Status = 201,
                    Message = "success",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] ProductUpdateModel model)
        {

            try
            {
                await productService.UpdateAsync(id, model);

                return Ok(new Response
                {
                    Status = 200,
                    Message = "success"
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await productService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            try
            {
                var product = await productService.GetAsync(id);

                return Ok(new Response<ProductViewModel>
                {
                    Status = 200,
                    Message = "success",
                    Data = product
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await productService.GetAllAsync();

                return Ok(new Response<IEnumerable<ProductViewModel>>
                {
                    Status = 200,
                    Message = "success",
                    Data = products
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = 500,
                    Message = ex.Message
                });
            }
        }
    }
}

