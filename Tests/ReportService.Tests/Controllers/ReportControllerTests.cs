using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ReportService.Clients.MessageBus;
using ReportService.Controllers;
using ReportService.DTOs;
using ReportService.Entities.Report;
using ReportService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportService.Tests.Controllers
{
    public class ReportControllerTests
    {
        #region Fields

        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly MessageBusClient _messageBusClient;

        #endregion Fields

        #region Ctor
        public ReportControllerTests()
        {
            _reportService = A.Fake<IReportService>();
            _messageBusClient = A.Fake<MessageBusClient>();
            _mapper = A.Fake<IMapper>();
        }
        #endregion

        #region Methods

        [Fact]
        public void ReportController_CreateReport_Return_Response200()
        {
            //Arrange
            var newReport = A.Fake<Report>();
            var newReportId = 1;
            var newReportDTO = A.Fake<ReportDTO>();
            A.CallTo(() => _reportService.AddReportAsync(newReport)).Returns(Task.FromResult(true));
            A.CallTo(() => _messageBusClient.Send(newReportId)).DoesNothing();
            A.CallTo(() => _mapper.Map<ReportDTO>(newReport)).Returns(newReportDTO);
            var controller = new ReportController(_reportService, _mapper, _messageBusClient);

            //Act
            var result = controller.CreateReport();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ReportController_CreateReport_Return_Response400()
        {
            //Arrange
            var controller = new ReportController(_reportService, _mapper, _messageBusClient);

            //Act
            controller.ModelState.AddModelError("Error", "Error");
            var result = controller.CreateReport();

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void ReportController_CreateReport_Return_Response500()
        {
            //Arrange
            var newReport = A.Fake<Report>();
            var newReportDTO = A.Fake<ReportDTO>();
            
            var controller = new ReportController(_reportService, _mapper, _messageBusClient);

            //Act
            var result = controller.CreateReport();

            //Assert
            A.CallTo(() => _reportService.AddReportAsync(newReport)).Returns(Task.FromResult(false)).Should().BeOfType(typeof(StatusCodeResult)).And.Be(500);
        }

        [Fact]
        public void ReportController_GetReports_Return_Response200()
        {
            //Arrange
            var reports = A.Fake<IEnumerable<Report>>();
            var reportsDTO = A.Fake<List<ReportDTO>>();
            A.CallTo(() => _reportService.GetReportsAsync()).Returns(Task.FromResult(reports));
            A.CallTo(() => _mapper.Map<List<ReportDTO>>(reports)).Returns(reportsDTO);
            var controller = new ReportController(_reportService, _mapper, _messageBusClient);

            //Act
            var result = controller.GetReports();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ReportController_GetReports_Return_Response400()
        {
            //Arrange
            var controller = new ReportController(_reportService, _mapper, _messageBusClient);

            //Act
            controller.ModelState.AddModelError("Error", "Error");
            var result = controller.GetReports();

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        #endregion

    }
}
