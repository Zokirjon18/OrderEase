using OrderEase.Service.Services.Reports.Models;

namespace OrderEase.Service.Services.Reports
{
    public interface IReportService
    {
        Task<ReportModel> CalculateMonthlyReportAsync();  
    }
}
