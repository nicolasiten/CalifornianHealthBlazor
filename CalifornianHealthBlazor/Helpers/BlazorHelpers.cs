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
        public static ValueTask InitializeBlazorHelpers<T>(IJSRuntime jsRuntime, DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.init", dotNetObjectReference);
        }

        public static ValueTask RenderCalendar(IJSRuntime jsRuntime, DotNetObjectReference<BookAppointment> dotNetObjectReference)
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.renderCalendar", dotNetObjectReference);
        }

        public static ValueTask UpdateAvailableAppointmentsInCalendar(IJSRuntime jsRuntime, string[] availableTimes)
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.updateAvailableAppointments", (object)availableTimes);
        }
    }
}
