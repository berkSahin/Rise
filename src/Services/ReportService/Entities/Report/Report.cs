using System;

namespace ReportService.Entities.Report
{
    public class Report : BaseEntity
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string ReportStatus { get; set; } = "Prep";
        public string ReportPath { get; set; }
    }
}