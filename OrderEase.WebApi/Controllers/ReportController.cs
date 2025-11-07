using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Services.Reports;
using OrderEase.Service.Services.Reports.Models;
using OrderEase.WebApi.Models;

namespace OrderEase.WebApi.Controllers;

public class ReportController(IReportService reportService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var report = await reportService.CalculateMonthlyReportAsync();

        return Ok(new Response<ReportModel>
        {
            Status = 200,
            Message = "success",
            Data = report
        });
    }
}
