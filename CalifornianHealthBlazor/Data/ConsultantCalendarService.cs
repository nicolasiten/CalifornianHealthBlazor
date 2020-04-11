using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data
{
    public class ConsultantCalendarService
    {
        public async Task<IEnumerable<string>> GetFreeAppointments(DateTime date, int consultantId)
        {
            return new[]
            {
                "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30", "14:00",
                "14:30", "15:00", "15:30", "16:00", "16.30"
            };
        }
    }
}
