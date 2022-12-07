using Microsoft.EntityFrameworkCore;
using ReportService.Data;
using ReportService.Entities.Report;
using ReportService.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repository
{
    public class ReportRepository : IReportRepository
    {
        #region Fields

        private readonly ReportDbContext _context;

        #endregion Fields

        #region Ctor

        public ReportRepository(ReportDbContext context)
        {
            _context = context;
        }

        #endregion Ctor

        #region Methods

        public async Task<bool> ReportExistAsync(int reportId)
        {
            return await _context.Reports.AnyAsync(a => a.Id == reportId);
        }

        public async Task<bool> AddReportAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            return await SaveAsync();
        }

        public async Task<bool> UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);
            return await SaveAsync();
        }

        public async Task<Report> GetReportAsync(int reportId)
        {
            return await _context.Reports.FindAsync(reportId);
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync<Report>();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        #endregion Methods
    }
}