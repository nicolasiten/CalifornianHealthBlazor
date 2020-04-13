using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public string Time { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
