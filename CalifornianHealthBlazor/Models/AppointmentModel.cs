using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Models
{
    public class AppointmentModel
    {
        public AppointmentModel()
        {
            Consultants = new List<ConsultantModel>();
        }

        public int SelectedConsultantId { get; set; }

        public DateTime SelectedDate { get; set; }

        public string SelectedTime { get; set; }

        public List<ConsultantModel> Consultants { get; private set; }
    }
}
