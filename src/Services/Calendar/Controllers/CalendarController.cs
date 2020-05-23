using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Interfaces;
using CalifornianHealth.Common.Exceptions;
using CalifornianHealth.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        /// <summary>
        /// Get new AppointmentModel with all Doctors and Patients.
        /// </summary>
        /// <returns>New AppointmentModel</returns>
        [HttpGet]
        [Route("GetAppointmentModel")]
        public async Task<AppointmentModel> GetAppointmentModel()
        {
            var appointmentModel = await _calendarService.BuildAppointmentModelAsync();

            return appointmentModel;
        }

        /// <summary>
        /// Get free appointment times for date and consultant.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="consultantId"></param>
        /// <returns>Free appointment times</returns>
        [HttpGet]
        [Route("GetFreeAppointmentTimes")]
        public async Task<IEnumerable<string>> GetFreeAppointmentTimes(DateTime date, int consultantId)
        {
            var freeAppointmentTimes = await _calendarService.GetFreeAppointmentTimesAsync(date, consultantId);

            return freeAppointmentTimes;
        }

        /// <summary>
        /// Get All Consultants
        /// </summary>
        /// <returns>All consultants</returns>
        [HttpGet]
        [Route("GetConsultants")]
        public async Task<IEnumerable<ConsultantModel>> GetConsultants()
        {
            var consultants = await _calendarService.GetConsultantsAsync();

            return consultants;
        }
    }
}