using Microsoft.AspNetCore.Http.HttpResults;
using OrderEase.Service.Exceptions;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(NotFoundException ex)
            {
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch(ArgumentIsNotValidException ex)
            {
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch(AlreadyExistsException ex)
            {
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Status = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch(Exception ex)
            {
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Status = 500,
                    Message = ex.Message
                });
            }
        }
    }
}
