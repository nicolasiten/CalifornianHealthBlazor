using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Amqp;
using Calendar.Interfaces;
using Calendar.Services;
using CalifornianHealthBlazor.Data;
using CalifornianHealthBlazor.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

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
            services.AddSingleton<IConnectionFactory>(provider =>
            {
                var configSection = Configuration.GetSection("amqp");
                return new ConnectionFactory
                {
                    HostName = configSection.GetValue<string>("HostName"),
                    UserName = configSection.GetValue<string>("UserName"),
                    Password = configSection.GetValue<string>("Password"),
                    AutomaticRecoveryEnabled = true
                };
            });
            services.AddSingleton<IConnection>(provider =>
            {
                var connectionFactory = provider.GetRequiredService<IConnectionFactory>();
                return connectionFactory.CreateConnection();
            });
            services.AddTransient<IModel>(provider =>
            {
                var connection = provider.GetRequiredService<IConnection>();
                return connection.CreateModel();
            });
            
            // services
            services.AddHostedService<BookingServer>();
            services.AddScoped<ICalendarService, CalendarService>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DataConnection")), ServiceLifetime.Transient);

            services.AddControllers();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
