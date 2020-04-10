using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CalifornianHealthBlazor.Helpers
{
    public static class BlazorHelpers
    {
        public static ValueTask RenderCalendar(IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeVoidAsync("blazorHelpers.renderCalendar");
        }
    }
}
