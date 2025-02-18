using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.SignalR.Helpers;

namespace MVCGrid.NetCore.SignalR
{
    public static class SignalRApplicationBuilderExtensions
    {
        public static void UseMvcGridSignalR(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MVCGridSignalR>("/MVCGridSignalR");
            });

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<MVCGridSignalR>("/MVCGridSignalR");
            //});
            SignalRHelper.Configure(app.ApplicationServices.GetRequiredService<IHubContext<MVCGridSignalR>>());
        }
    }
}
