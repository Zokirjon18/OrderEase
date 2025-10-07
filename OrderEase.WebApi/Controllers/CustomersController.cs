using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Customers;
using OrderEase.Service.Services.Customers.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync(CustomerCreateModel model)
        {
            try
            {
                await customerService.CreateAsync(model);

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
        public async Task<IActionResult> PutAsync(int id, [FromBody] CustomerUpdateModel model)
        {

            try
            {
                await customerService.UpdateAsync(id, model);

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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await customerService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var customer = await customerService.GetAsync(id);

                return Ok(new Response<CustomerViewModel>
                {
                    Status = 200,
                    Message = "success",
                    Data = customer
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
                var customers = await customerService.GetAllAsync();

                return Ok(new Response<List<CustomerViewModel>>
                {
                    Status = 200,
                    Message = "success",
                    Data = customers
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
