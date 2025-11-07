using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Services.Auth;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers
{
    public class AuthController(IAuthService authService) : BaseController
    {
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] string userId, string phone)
        {
            var token = authService.GenerateToken(userId, phone);

            return Ok(new Response<string>
            {
                Status = 201,
                Message = "success",
                Data = token
            });
        }
    }
}
