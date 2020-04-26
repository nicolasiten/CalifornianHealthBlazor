using System;
using System.Collections.Generic;
using System.Text;

namespace CalifornianHealth.Common.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }
    }
}
