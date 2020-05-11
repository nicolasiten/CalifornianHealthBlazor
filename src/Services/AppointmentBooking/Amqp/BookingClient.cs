using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBooking.Interfaces;
using CalifornianHealth.Common.Amqp.Booking;
using EasyNetQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IConnection = RabbitMQ.Client.IConnection;

namespace AppointmentBooking.Amqp
{
    public class BookingClient : IBookingClient
    {
        //private readonly IConnection _channel;
        private readonly IBus _bus;

        //public BookingClient(IConnection connection)
        public BookingClient(IBus bus)
        {
            //_channel = connection;
            _bus = bus;
        }

        public async Task<string> SendBooking(string message)
        {
            //return await Task.Run(() =>
            //{
            var result = await _bus.RequestAsync<BookingRequest, BookingResponse>(new BookingRequest
            {
                Booking = message
            }).ConfigureAwait(false);

            return result.Response;
            //using var channel = _channel.CreateModel();
            //BlockingCollection<string> responseQueue = new BlockingCollection<string>();
            //string replyQueueName = channel.QueueDeclare().QueueName;
            //var consumer = new AsyncEventingBasicConsumer(channel);
            //IBasicProperties properties = channel.CreateBasicProperties();
            //var correlationId = Guid.NewGuid().ToString();
            //properties.CorrelationId = correlationId;
            //properties.ReplyTo = replyQueueName;

            //consumer.Received += async (sender, args) =>
            //{
            //    if (args.BasicProperties.CorrelationId == properties.CorrelationId)
            //    {
            //        responseQueue.Add(Encoding.UTF8.GetString(args.Body.ToArray()));
            //    }

            //    await Task.Yield();
            //};

            ////properties.CorrelationId = hubConnection.ConnectionId;
            //var messageBytes = Encoding.UTF8.GetBytes(message);
            //channel.BasicPublish(string.Empty, "booking_queue", properties, messageBytes);

            //channel.BasicConsume(consumer, replyQueueName, true);

            //var result = responseQueue.Take();
            //return result;
            //}).ConfigureAwait(false);
        }
    }
}
