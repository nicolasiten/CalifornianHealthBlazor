using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calendar.Interfaces;
using CalifornianHealth.Common.Amqp.Booking;
using CalifornianHealth.Common.Models;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calendar.Amqp
{
    public class BookingServer : IHostedService
    {
        //private readonly IModel _channel;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBus _bus;

        //public BookingServer(IModel channel, IServiceScopeFactory serviceScopeFactory)
        public BookingServer(IBus bus)
        {
            _bus = bus;
            //_channel = channel;
            //_serviceScopeFactory = serviceScopeFactory;
        }

        //private void Setup()
        //{
        //    _channel.QueueDeclare("booking_queue", false, false, false, null);
        //    _channel.BasicQos(0, 500, true);

        //    var consumer = new EventingBasicConsumer(_channel);
        //    _channel.BasicConsume("booking_queue", false, consumer);

        //    consumer.Received += async (sender, e) => await BookingReceived(sender, e);
        //}

        //private async Task BookingReceived(object sender, BasicDeliverEventArgs e)
        //{
        //    var body = e.Body;
        //    var properties = e.BasicProperties;
        //    var replyProperties = _channel.CreateBasicProperties();
        //    replyProperties.CorrelationId = properties.CorrelationId;
        //    var response = "Ok";

        //    try
        //    {
        //        //string bodyString = Encoding.UTF8.GetString(body.ToArray());
        //        //var appointmentModel = JsonConvert.DeserializeObject<AppointmentModel>(bodyString);

        //        //using (var scope = _serviceScopeFactory.CreateScope())
        //        //{
        //        //    var calendarService = scope.ServiceProvider.GetService<ICalendarService>();
        //        //    await calendarService.SaveAppointmentAsync(appointmentModel);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        response = ex.Message;
        //    }
        //    finally
        //    {
        //        var responseBytes = Encoding.UTF8.GetBytes(response);
        //        _channel.BasicPublish(string.Empty, properties.ReplyTo, replyProperties, responseBytes);
        //        _channel.BasicAck(e.DeliveryTag, false);
        //    }
        //}

        private Task<BookingResponse> Response(BookingRequest request)
        {
            return Task.FromResult(new BookingResponse { Response = "Ok" });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Setup();
            _bus.Respond<BookingRequest, BookingResponse>(request => { return new BookingResponse {Response = "Ok"}; });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
