using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calendar.Extensions;
using Calendar.Interfaces;
using CalifornianHealth.Common.Amqp.Booking;
using CalifornianHealth.Common.Exceptions;
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
        private readonly IBus _bus;
        private readonly IAppointmentDataHandler _appointmentDataHandler;

        public BookingServer(IBus bus, IAppointmentDataHandler appointmentDataHandler)
        {
            _bus = bus;
            _appointmentDataHandler = appointmentDataHandler;
        }

        private Task<BookingResponse> Response(BookingRequest request)
        {
            return Task.FromResult(new BookingResponse { Response = "Ok" });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            int maxRetries = 5;
            int retries = 0;

            while (retries < maxRetries)
            {
                try
                {
                    _bus.RespondAsync<BookingRequest, BookingResponse>(RespondAsync);
                    break;
                }
                catch (Exception)
                {
                    if (retries >= maxRetries)
                    {
                        throw;
                    }

                    retries++;
                    await Task.Delay(5000, cancellationToken);
                }
            }
        }

        private async Task<BookingResponse> RespondAsync(BookingRequest request)
        {
            if (JsonUtils.TryParse<AppointmentModel>(request.Booking, out var appointmentModel))
            {
                try
                {
                    await _appointmentDataHandler.SaveAppointmentAsync(appointmentModel);
                    return new BookingResponse { Response = "Ok" };
                }
                catch (CalifornianHealthException ex)
                {
                    return new BookingResponse { Response = ex.Message };
                }
            }

            return new BookingResponse { Response = "Invalid request. Couldn't parse object!" };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
