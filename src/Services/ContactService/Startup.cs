using ContactService.Data;
using ContactService.Repository;
using ContactService.Repository.Interfaces;
using ContactService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ContactService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ContactDB"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
            services.AddScoped<IContactInfoTypeRepository, ContactInfoTypeRepository>();
            services.AddScoped<IContactService, ContactService.Services.ContactService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpgradeDatabase(app);
            SeedData.Seed(app);
        }

        private void UpgradeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ContactDbContext>();
                if (context != null && context.Database != null)
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}