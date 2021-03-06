﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealth.Common.Models;

namespace Calendar.Interfaces
{
    public interface ICalendarService
    {
        Task<AppointmentModel> BuildAppointmentModelAsync();

        Task<IEnumerable<string>> GetFreeAppointmentTimesAsync(DateTime date, int consultantId);

        Task<IEnumerable<ConsultantModel>> GetConsultantsAsync();
    }
}
