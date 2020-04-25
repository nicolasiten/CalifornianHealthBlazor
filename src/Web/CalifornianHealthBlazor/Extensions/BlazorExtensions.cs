using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CalifornianHealthBlazor.Extensions
{
    public static class BlazorExtensions
    {
        public static ValueTask NavigateToFragmentAsync(this NavigationManager navigationManager, IJSRuntime jSRuntime)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (uri.Fragment.Length == 0)
            {
                return default;
            }
            return jSRuntime.InvokeVoidAsync("blazorHelpers.scrollToFragment", uri.Fragment.Substring(1));
        }
    }
}
