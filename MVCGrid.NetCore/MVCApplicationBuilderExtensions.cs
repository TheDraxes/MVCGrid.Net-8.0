﻿using Microsoft.AspNetCore.Builder;
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
                else
                {
                    string gridName = context.Request.Query["Name"];
                    IMVCGridDefinition grid = MVCGridDefinitionTable.GetDefinitionInterface(gridName);
                    QueryOptions options = QueryStringParser.ParseOptions(grid, context.Request.ToNameValueCollection());
                    GridContext gridContext = MVCGrid.NetCore.Utility.GridContextUtility.Create(/*context, */gridName, grid, options);

                    GridEngine engine = new GridEngine();
                    if (!engine.CheckAuthorization(gridContext))
                    {
                        //Forbidden
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync(string.Empty);
                        return;
                    }

                    IMVCGridRenderingEngine renderingEngine = GridEngine.GetRenderingEngine(gridContext);

                    // TODO: Reimplement this for csv exports and other rendering responses.
                    //renderingEngine.PrepareResponse(context.Response);

                    StringBuilder sb = new StringBuilder();
                    TextWriter htmlWriter = new StringWriter(sb);
                    engine.Run(renderingEngine, gridContext, htmlWriter);
                    string html = sb.ToString();
                    await context.Response.WriteAsync(html);
                }
            });
        }

        public static void HandleMVCGridScript(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string script = GetResourceFileContentAsString("MVCGrid", "Scripts/MVCGrid.js");
                script = script.Replace("%%CONTROLLERPATH%%", "gridmvc/grid");
                script = script.Replace("%%ERRORDETAILS%%", "''");
                script = script.Replace("%%HANDLERPATH%%", "../MVCGrid");
                context.Response.ContentType = "text/javascript";
                await context.Response.WriteAsync(script);
            });
        }

        public static void UseMvcGrid(this IApplicationBuilder app)
        {
            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            GridRegistration.RegisterAllGrids();
            
            app.Map("/MVCGrid.js", HandleMVCGridScript);
            app.Map("/MVCGrid", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/sortup.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/sortdown.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/sort.png", HandleMVCGrid);
            app.Map("/MVCGridHandler.axd/ajaxloader.gif", HandleMVCGrid);
            app.Map("/ajaxloader.gif", HandleMVCGrid);
        }
    }
}
