using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calendar.Interfaces;
using CalifornianHealth.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calendar.Amqp
{
    public class BookingServer : IHostedService
    {
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BookingServer(IModel channel, IServiceScopeFactory serviceScopeFactory)
        {
            _channel = channel;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private void Setup()
        {
            _channel.QueueDeclare("booking_queue", false, false, false, null);
            _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume("booking_queue", false, consumer);

            consumer.Received += async (sender, e) => await BookingReceived(sender, e);
        }

        private async Task BookingReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var properties = e.BasicProperties;
            var replyProperties = _channel.CreateBasicProperties();
            replyProperties.CorrelationId = properties.CorrelationId;
            var response = "Ok";

            try
            {
                string bodyString = Encoding.UTF8.GetString(body.ToArray());
                var appointmentModel = JsonConvert.DeserializeObject<AppointmentModel>(bodyString);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var appointmentService = scope.ServiceProvider.GetService<IAppointmentService>();
                    await appointmentService.SaveAppointmentAsync(appointmentModel);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                _channel.BasicPublish(string.Empty, properties.ReplyTo, replyProperties, responseBytes);
                _channel.BasicAck(e.DeliveryTag, false);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Setup();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
