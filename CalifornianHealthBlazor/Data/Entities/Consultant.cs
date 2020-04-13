using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data.Entities
{
    public class Consultant : BaseEntity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Specialty { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
