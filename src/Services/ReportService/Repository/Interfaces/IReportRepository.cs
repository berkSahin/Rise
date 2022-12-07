using ReportService.Entities.Report;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repository.Interfaces
{
    public interface IReportRepository
    {
        Task<bool> ReportExistAsync(int reportId);

        Task<bool> AddReportAsync(Report report);

        Task<bool> UpdateReportAsync(Report report);

        Task<Report> GetReportAsync(int reportId);

        Task<IEnumerable<Report>> GetReportsAsync();

        Task<bool> SaveAsync();
    }
}