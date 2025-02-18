using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
