using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBooking.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IConnection = RabbitMQ.Client.IConnection;

namespace AppointmentBooking.Amqp
{
    public class BookingClient : IBookingClient
    {
        private readonly IConnection _channel;

        public BookingClient(IConnection model)
        {
            _channel = model;
        }

        public async Task<string> SendBooking(string message)
        {
            //return await Task.Run(() =>
            //{
                using var channel = _channel.CreateModel();
                BlockingCollection<string> responseQueue = new BlockingCollection<string>();
                string replyQueueName = channel.QueueDeclare().QueueName;
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                IBasicProperties properties = channel.CreateBasicProperties();
                var correlationId = Guid.NewGuid().ToString();
                properties.CorrelationId = correlationId;
            properties.ReplyTo = replyQueueName;

            consumer.Received += (sender, args) =>
            {
                if (args.BasicProperties.CorrelationId == properties.CorrelationId)
                {
                    responseQueue.Add(Encoding.UTF8.GetString(args.Body.ToArray()));
                }
            };

            //properties.CorrelationId = hubConnection.ConnectionId;
            var messageBytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, "booking_queue", properties, messageBytes);

                channel.BasicConsume(consumer, replyQueueName, true);
                
                var result = responseQueue.Take();
                return result;
            //}).ConfigureAwait(false);
        }
    }
}
