using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Clients.Contact;
using ReportService.Services.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportService.Utils
{
    public class QueueWorker : BackgroundService
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;
        private readonly IContactHttpClient _contactHttpClient;
        private IConnection _connection;
        private IModel _channel;

        #endregion Fields

        #region Ctor

        public QueueWorker(IContactHttpClient contactHttpClient, IServiceProvider serviceProvider)
        {
            _contactHttpClient = contactHttpClient;
            _serviceProvider = serviceProvider;
            InitRabbitMQ();
        }

        #endregion Ctor

        #region Methods

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "admin",
                Password = "admin"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("reports",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var reportId = BitConverter.ToInt32(body.ToArray());

                System.Diagnostics.Debug.WriteLine("ReportId: " + reportId);
                GenerateReport(reportId).Wait();
            };

            _channel.BasicConsume(queue: "reports", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task GenerateReport(int reportId)
        {
            var filename = reportId.ToString() + ".csv";

            var sb = new StringBuilder();
            sb.AppendLine("Location;Contact Count;Phonenumber Count");

            var contacts = _contactHttpClient.GetContactsAsync().Result;
            var locations = contacts.Where(w => w.ContactInfos.Any(a => a.ContactInfoTypeId == 3)).SelectMany(s => s.ContactInfos).Where(a => a.ContactInfoTypeId == 3).Select(s => s.Value).Distinct();

            foreach (var location in locations)
            {
                var contactCount = contacts.Count(c => c.ContactInfos.Any(a => a.Value.Equals(location)));
                var phoneNumberCount = contacts.Where(w => w.ContactInfos.Any(a => a.Value.Equals(location))).Where(ww => ww.ContactInfos.Any(aa => aa.ContactInfoTypeId == 1)).Count();
                sb.AppendLine(string.Format("{0};{1};{2}", location, contactCount, phoneNumberCount));
            }

            System.IO.File.WriteAllText(System.IO.Path.Combine("/app/reports", filename), sb.ToString());

            using IServiceScope scope = _serviceProvider.CreateScope();
            IReportService reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

            var report = await reportService.GetReportAsync(reportId);
            report.ReportStatus = "Comp";
            report.ReportPath = System.IO.Path.Combine("api/DownloadReport", filename);
            await reportService.UpdateReportAsync(report);
        }

        #endregion Methods
    }
}