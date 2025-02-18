using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.SignalR.Helpers;

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
