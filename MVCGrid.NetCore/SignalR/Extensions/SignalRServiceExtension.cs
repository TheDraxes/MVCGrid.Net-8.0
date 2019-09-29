using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.Helpers;
using MVCGrid.NetCore.SignalR.Helpers;
using MVCGrid.Web;
using System;

namespace MVCGrid.NetCore
{
    public static class SignalRServiceExtension
    {
        public static void AddMvcGridSignalR(this IServiceCollection services)
        {
            services.AddScoped<SignalRHelper>();
        }
    }
}
