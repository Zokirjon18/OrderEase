using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Services.Reports;
using OrderEase.Service.Services.Reports.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController(IReportService reportService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var report = await reportService.CalculateMonthlyReportAsync();

                return Ok(new Response<ReportModel>
                {
                    Status = 200,
                    Message = "success",
                    Data = report
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Status = 500,
                    Message = ex.Message
                });
            }
        }
    }
}
