using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportService.Clients.MessageBus;
using ReportService.DTOs;
using ReportService.Entities.Report;
using ReportService.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        #region Fields

        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly MessageBusClient _messageBusClient;

        #endregion Fields

        #region Ctor

        public ReportController(IReportService reportService, IMapper mapper, MessageBusClient messageBusClient)
        {
            _reportService = reportService;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        #endregion Ctor

        #region Methods

        [HttpGet("CreateReport")]
        [ProducesResponseType(200, Type = typeof(Report))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReport()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newReport = new Report();

            if (!(await _reportService.AddReportAsync(newReport)))
            {
                return StatusCode(500);
            }

            _messageBusClient.Send(newReport.Id);
            return Ok(_mapper.Map<ReportDTO>(newReport));
        }

        [HttpGet("GetReports")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Report>))]
        public async Task<IActionResult> GetReports()
        {
            var reports = _mapper.Map<List<ReportDTO>>(await _reportService.GetReportsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reports);
        }

        [HttpGet("GetReportDetail/{reportId}")]
        [ProducesResponseType(200, Type = typeof(Report))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReportDetail(int reportId)
        {
            if (!(await _reportService.ReportExistAsync(reportId)))
                return NotFound();

            var report = _mapper.Map<ReportDetailsDTO>(await _reportService.GetReportAsync(reportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(report);
        }

        [HttpGet("DownloadReport/{path}")]
        [ProducesResponseType(200, Type = typeof(Report))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DownloadReport(string path)
        {
            var filepath = Path.Combine("reports", path);

            if (!System.IO.File.Exists(filepath))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var file = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(file, "application/vnd.ms-excel", path);
        }

        #endregion Methods
    }
}