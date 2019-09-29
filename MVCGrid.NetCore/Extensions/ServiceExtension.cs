using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.Helpers;
using MVCGrid.Web;
using System;

namespace MVCGrid.NetCore
{
    public static class MVCServiceExtension
    {
        public static IMvcBuilder AddMvcGrid(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return null;
        }
    }
}
