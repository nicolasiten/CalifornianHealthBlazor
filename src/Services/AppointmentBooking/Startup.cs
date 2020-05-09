using AppointmentBooking.Amqp;
using AppointmentBooking.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddSingleton<IConnection>(provider =>
            {
                var configSection = Configuration.GetSection("amqp");
                var connectionFactory = new ConnectionFactory
                {
                    HostName = configSection.GetValue<string>("HostName"),
                    UserName = configSection.GetValue<string>("UserName"),
                    Password = configSection.GetValue<string>("Password")
                };
                return connectionFactory.CreateConnection();
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
