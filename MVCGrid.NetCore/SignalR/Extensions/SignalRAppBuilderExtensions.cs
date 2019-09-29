using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using MVCGrid.Web;
using MVCGrid.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.Helpers;
using MVCGrid.Utility;
using MVCGrid.NetCore.Engine;
using MVCGrid.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MVCGrid.NetCore.SignalR.Helpers;

namespace MVCGrid.NetCore.SignalR
{
    public static class SignalRApplicationBuilderExtensions
    {
        public static void UseMvcGridSignalR(this IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<MVCGridSignalR>("/MVCGridSignalR");
            });
            SignalRHelper.Configure(app.ApplicationServices.GetRequiredService<IHubContext<MVCGridSignalR>>());
        }
    }
}
