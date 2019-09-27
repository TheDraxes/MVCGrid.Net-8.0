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

namespace MVCGrid.NetCore
{
    public static class MVCApplicationBuilderExtensions
    {
        private static void HandleScriptJs(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string script = GetResourceFileContentAsString("MVCGrid", "Scripts/MVCGrid.js");
                await context.Response.WriteAsync(script);
            });
        }

        static Stream GetResourceStream(string resourcePath)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "MVCGrid");
            List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());

            resourcePath = resourcePath.Replace(@"/", ".");
            resourcePath = resourceNames.FirstOrDefault(r => r.Contains(resourcePath));

            if (resourcePath == null)
                throw new FileNotFoundException("Resource not found");

            return assembly.GetManifestResourceStream(resourcePath);
        }
        public static string GetResourceFileContentAsString(string thenamespace, string fileName)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "MVCGrid");
            var resourceName = thenamespace + "." + fileName;

            string resource = null;
            using (Stream stream = GetResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    resource = reader.ReadToEnd();
                }
            }
            return resource;
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
