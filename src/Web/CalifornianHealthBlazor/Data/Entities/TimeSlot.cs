using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data.Entities
{
    public class TimeSlot : BaseEntity
    {
        public int? ConsultantFk { get; set; }

        public string Time { get; set; }

        public int DayOfWeek { get; set; }

        public virtual Consultant Consultant { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
