using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Calendar.Amqp;
using Calendar.Data;
using Calendar.Interfaces;
using Calendar.Services;
using CalifornianHealthBlazor.Data;
using CalifornianHealthBlazor.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Calendar
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
            // amqp
            var configSection = Configuration.GetSection("amqp");
            services.RegisterEasyNetQ($"host={configSection.GetValue<string>("HostName")};username={configSection.GetValue<string>("UserName")};password={configSection.GetValue<string>("Password")}");

            // services
            services.AddHostedService<BookingServer>();
            services.AddScoped<ICalendarService, CalendarService>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DataConnection")), ServiceLifetime.Transient);

            services.AddSingleton<IAppointmentDataHandler, AppointmentDataHandler>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Calendar Service", Version = "v1"});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar Service");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
