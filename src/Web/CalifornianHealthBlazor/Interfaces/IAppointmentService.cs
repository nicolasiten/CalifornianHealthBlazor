using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealth.Common.Models;

namespace CalifornianHealthBlazor.Interfaces
{
    public interface IAppointmentService
    {
        Task BuildAppointmentModelAsync(AppointmentModel appointmentModel);

        IEnumerable<string> ValidateAppointment(AppointmentModel appointmentModel);

        Task<IEnumerable<string>> GetFreeAppointmentTimesAsync(DateTime date, int consultantId);

        Task SaveAppointmentAsync(AppointmentModel appointmentModel);
    }
}
