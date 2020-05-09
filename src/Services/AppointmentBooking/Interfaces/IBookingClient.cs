using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Interfaces
{
    public interface IBookingClient
    {
        Task<string> SendBooking(string message);
    }
}
