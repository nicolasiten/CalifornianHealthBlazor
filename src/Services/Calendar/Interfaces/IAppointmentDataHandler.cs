using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealth.Common.Models;

namespace Calendar.Interfaces
{
    public interface IAppointmentDataHandler
    {
        Task SaveAppointmentAsync(AppointmentModel appointmentModel);
    }
}
