using ReportService.Entities.Report;
using ReportService.Repository.Interfaces;
using ReportService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public class ReportService : IReportService
    {
        #region Fields

        private readonly IReportRepository _reportRepository;

        #endregion Fields

        #region Ctor

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        #endregion Ctor

        #region Methods

        public async Task<bool> ReportExistAsync(int reportId)
        {
            return await _reportRepository.ReportExistAsync(reportId);
        }

        public async Task<bool> AddReportAsync(Report report)
        {
            return await _reportRepository.AddReportAsync(report);
        }

        public async Task<bool> UpdateReportAsync(Report report)
        {
            return await _reportRepository.UpdateReportAsync(report);
        }

        public async Task<Report> GetReportAsync(int reportId)
        {
            return await _reportRepository.GetReportAsync(reportId);
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            return await _reportRepository.GetReportsAsync();
        }

        #endregion Methods
    }
}