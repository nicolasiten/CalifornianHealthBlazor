using System.Threading.Tasks;
using AppointmentBooking.Interfaces;
using CalifornianHealth.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        /// <summary>
        /// Book an appointment.
        /// Appointment will be appended to AMQP Queue and processed by Calendar Service.
        /// </summary>
        /// <param name="appointmentModel"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public async Task<ActionResult> SaveAppointment(AppointmentModel appointmentModel)
        {
            var response = await _bookingClient.SendBooking(JsonConvert.SerializeObject(appointmentModel)).ConfigureAwait(false);

            if (response == "Ok")
            {
                return Ok();
            }

            return BadRequest(response);
        }
    }
}