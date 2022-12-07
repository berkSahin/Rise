using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReportService.Clients.Contact;
using ReportService.Clients.MessageBus;
using ReportService.Data;
using ReportService.Repository;
using ReportService.Repository.Interfaces;
using ReportService.Services.Interfaces;
using ReportService.Utils;
using System;

namespace ReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddDbContext<ReportDbContext>(options =>
            //{
            //    options.UseLazyLoadingProxies();
            //    options.UseInMemoryDatabase("ReportDb");
            //});

            services.AddDbContext<ReportDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ReportDB"));
                options.UseLazyLoadingProxies();
            });

            services.AddHttpClient<IContactHttpClient, ContactHttpClient>();

            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportService.Services.ReportService>();

            services.AddSingleton<MessageBusClient>();
            services.AddHostedService<QueueWorker>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReportService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            UpgradeDatabase(app);
        }

        private void UpgradeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ReportDbContext>();
                if (context != null && context.Database != null)
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}