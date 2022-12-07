using System;

namespace ReportService.DTOs
{
    public class ReportDetailsDTO
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string ReportStatus { get; set; }
        public string ReportPath { get; set; }
    }
}