using System;
using System.Collections.Generic;
using System.Text;

namespace CalifornianHealth.Common.Models
{
    public class AppointmentModel
    {
        public AppointmentModel()
        {
            Consultants = new List<ConsultantModel>();
            Patients = new List<PatientModel>();
        }

        public int SelectedConsultantId { get; set; }

        public int SelectedPatientId { get; set; }

        public DateTime SelectedDate { get; set; }

        public string SelectedTime { get; set; }

        public List<ConsultantModel> Consultants { get; private set; }

        public List<PatientModel> Patients { get; private set; }
    }
}
