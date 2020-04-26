using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBooking.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AppointmentBooking.Amqp
{
    public class BookingClient : IBookingClient
    {
        private readonly IModel _channel;
        private readonly IBasicProperties _properties;

        private readonly EventingBasicConsumer _consumer;
        private readonly string _replyQueueName;
        private readonly BlockingCollection<string> _responseQueue = new BlockingCollection<string>();

        public BookingClient(IModel channel)
        {
            _channel = channel;

            _replyQueueName = _channel.QueueDeclare().QueueName;
            _consumer = new EventingBasicConsumer(_channel);

            _properties = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            _properties.CorrelationId = correlationId;
            _properties.ReplyTo = _replyQueueName;

            _consumer.Received += MessageReceived;
        }

        public string SendBooking(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(string.Empty, "booking_queue", _properties, messageBytes);

            _channel.BasicConsume(_consumer, _replyQueueName, true);

            return _responseQueue.Take();
        }

        private void MessageReceived(object sender, BasicDeliverEventArgs e)
        {
            if (e.BasicProperties.CorrelationId == _properties.CorrelationId)
            {
                _responseQueue.Add(Encoding.UTF8.GetString(e.Body.ToArray()));
            }
        }
    }
}
