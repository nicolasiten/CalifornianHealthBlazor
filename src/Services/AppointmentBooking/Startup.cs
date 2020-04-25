using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentBooking.Amqp;
using AppointmentBooking.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace AppointmentBooking
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
                    Password = configSection.GetValue<string>("Password")
                };
            });
            services.AddTransient<IConnection>(provider =>
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
            services.AddTransient<IBookingClient, BookingClient>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
