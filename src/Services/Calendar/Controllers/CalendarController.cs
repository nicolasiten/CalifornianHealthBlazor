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
        private readonly IAppointmentService _appointmentService;

        public CalendarController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("GetAppointmentModel")]
        public async Task<AppointmentModel> GetAppointmentModel()
        {
            var appointmentModel = await _appointmentService.BuildAppointmentModelAsync();

            return appointmentModel;
        }

        [HttpGet]
        [Route("GetFreeAppointmentTimes")]
        public async Task<IEnumerable<string>> GetFreeAppointmentTimes(DateTime date, int consultantId)
        {
            var freeAppointmentTimes = await _appointmentService.GetFreeAppointmentTimesAsync(date, consultantId);

            return freeAppointmentTimes;
        }

        [HttpGet]
        [Route("GetConsultants")]
        public async Task<IEnumerable<ConsultantModel>> GetConsultants()
        {
            var consultants = await _appointmentService.GetConsultantsAsync();

            return consultants;
        }
    }
}