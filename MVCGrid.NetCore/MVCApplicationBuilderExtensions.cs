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

namespace MVCGrid.NetCore
{
    public static class MVCApplicationBuilderExtensions
    {
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
        static string GetResourceFileContentAsString(string thenamespace, string fileName)
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
        static byte[] GetResourceFileContentAsByteArray(string thenamespace, string fileName)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "MVCGrid");
            var resourceName = thenamespace + "." + fileName;

            byte[] bytes;
            using (Stream stream = GetResourceStream(resourceName))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }
            }
            return bytes;
        }

        public static void RegisterMVCGrid<T>(this IApplicationBuilder app, string name, MVCGridBuilder<T> builder)
        {
            MVCGridDefinitionTable.Add(name, builder);
        }

        public static void HandleMVCGrid(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string path = context.Request.PathBase.Value;
                if (path.Contains(".gif") || path.Contains(".png") || path.Contains(".jpg"))
                {
                    path = "Images" + path.Replace("/MVCGridHandler.axd", string.Empty);
                    byte[] image = GetResourceFileContentAsByteArray("MVCGrid", path);
                    context.Response.ContentType = "image/png";
                    await context.Response.Body.WriteAsync(image, 0, image.Length);
                }
            });
        }

        public static void HandleMVCGridScript(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string script = GetResourceFileContentAsString("MVCGrid", "Scripts/MVCGrid.js");
                script = script.Replace("%%CONTROLLERPATH%%", "gridmvc/grid");
                context.Response.ContentType = "text/javascript";
                await context.Response.WriteAsync(script);
            });
        }

        public static void UseMvcGrid(this IApplicationBuilder app)
        {
            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            GridRegistration.RegisterAllGrids();
            
            app.Map("/MVCGrid.js", HandleMVCGridScript);
            app.Map("/MVCGridHandler.axd/sortup.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/sortdown.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/sort.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/ajaxloader.gif", HandleMVCGrid);
            app.Map("/ajaxloader.gif", HandleMVCGrid);
        }
    }
}
