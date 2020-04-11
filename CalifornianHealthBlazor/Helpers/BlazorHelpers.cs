using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Pages;
using Microsoft.JSInterop;

namespace CalifornianHealthBlazor.Helpers
{
    public static class BlazorHelpers
    {
        public static ValueTask RenderCalendar(IJSRuntime jsRuntime, BookAppointment bookAppointment)
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.renderCalendar", DotNetObjectReference.Create(bookAppointment));
        }

        public static ValueTask UpdateAvailableAppointmentsInCalendar(IJSRuntime jsRuntime, string[] availableTimes)
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.updateAvailableAppointments", availableTimes);
        }
    }
}
