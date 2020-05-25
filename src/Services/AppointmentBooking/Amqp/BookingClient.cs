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
        private readonly IBus _bus;

        public BookingClient(IBus bus)
        {
            _bus = bus;
        }

        public async Task<string> SendBooking(string message)
        {
            var result = await _bus.RequestAsync<BookingRequest, BookingResponse>(new BookingRequest
            {
                Booking = message
            });

            return result.Response;
        }
    }
}
