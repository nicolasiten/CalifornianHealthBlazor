using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBooking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AppointmentBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentBookingController : ControllerBase
    {
        private readonly IBookingClient _bookingClient;

        public AppointmentBookingController(IBookingClient bookingClient)
        {
            _bookingClient = bookingClient;
        }

        [HttpGet]
        public ActionResult Test()
        {
            var response = _bookingClient.SendBooking("Test");

            return Ok(response);
        }
    }
}