using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Data.Entities
{
    public class Patient : BaseEntity
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
