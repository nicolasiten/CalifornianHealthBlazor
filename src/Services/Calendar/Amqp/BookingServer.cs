﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calendar.Amqp
{
    public class BookingServer : IHostedService
    {
        private readonly IModel _channel;

        public BookingServer(IModel channel)
        {
            _channel = channel;
        }

        private void Setup()
        {
            _channel.QueueDeclare("booking_queue", false, false, false, null);
            _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume("booking_queue", false, consumer);

            consumer.Received += BookingReceived;
        }

        private void BookingReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var properties = e.BasicProperties;
            var replyProperties = _channel.CreateBasicProperties();
            replyProperties.CorrelationId = properties.CorrelationId;
            var response = "OK";

            try
            {
                // TODO try save booking
            }
            catch (Exception ex)
            {
                // Handle
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