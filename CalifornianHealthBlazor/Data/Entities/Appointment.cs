using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int? ConsultantFk { get; set; }

        public int? PatientFk { get; set; }

        public int? TimeSlotFk { get; set; }

        public DateTime SelectedDate { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual TimeSlot TimeSlot { get; set; }
    }
}
