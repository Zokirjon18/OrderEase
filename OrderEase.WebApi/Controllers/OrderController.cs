using Microsoft.AspNetCore.Mvc;
using OrderEase.Domain.Enums;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Orders;
using OrderEase.Service.Services.Orders.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync(OrderCreateModel model)
        {
            try
            {
                await orderService.CreateAsync(model);

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


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await orderService.CancelAsync(id);

                return Ok();
            }
            catch (ArgumentIsNotValidException ex)
            {
                return BadRequest(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
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
                var product = await orderService.GetAsync(id);

                return Ok(new Response<OrderViewModel>
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
        public async Task<IActionResult> GetAllAsync(long? customerId = null, OrderStatus? status = null)
        {
            try
            {
                var products = await orderService.GetAllAsync(customerId, status);

                return Ok(new Response<IEnumerable<OrderViewModel>>
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

