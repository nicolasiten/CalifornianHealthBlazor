using System;
using System.Collections.Generic;
using System.Text;

namespace JmeterLoadTestGenerator
{
    public class Request
    {
        public int SelectedConsultantId { get; set; }

        public int SelectedPatientId { get; set; }

        public DateTime SelectedDate { get; set; }

        public string SelectedTime { get; set; }
    }
}
