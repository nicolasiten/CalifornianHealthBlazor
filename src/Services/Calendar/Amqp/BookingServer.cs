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
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBus _bus;

        public BookingServer(IBus bus)
        {
            _bus = bus;
            //_serviceScopeFactory = serviceScopeFactory;
        }

        private Task<BookingResponse> Response(BookingRequest request)
        {
            return Task.FromResult(new BookingResponse { Response = "Ok" });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _bus.Respond<BookingRequest, BookingResponse>(request => { return new BookingResponse {Response = "Ok"}; });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
