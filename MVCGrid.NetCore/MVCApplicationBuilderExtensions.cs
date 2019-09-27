using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using MVCGrid.Web;
using MVCGrid.Models;

namespace MVCGrid.NetCore
{
    public static class MVCApplicationBuilderExtensions
    {
        private static void HandleScriptJs(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string script = GetScript();
                await context.Response.WriteAsync(script);
            });
        }

        public static string GetScript()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MVCGrid.Scripts.js";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static IApplicationBuilder RegisterMVCGrid<T>(this IApplicationBuilder app, string name, MVCGridBuilder<T> builder)
        {
            MVCGridDefinitionTable.Add(name, builder);
            return app;
        }

        public static IApplicationBuilder UseMvcGrid(this IApplicationBuilder app)
        {
            GridRegistration.RegisterAllGrids();
            return app.Map("/MVCGrid.js", HandleScriptJs);
        }
    }
}
