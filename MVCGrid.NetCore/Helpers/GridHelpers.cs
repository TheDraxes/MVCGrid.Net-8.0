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
using System.Collections.Specialized;

namespace MVCGrid.NetCore.Helpers
{
    internal static class GridHelpers
    {
        internal static string GenerateGrid(string gridName, out int statusCode, NameValueCollection nameValueCollection = null)
        {
            if (nameValueCollection == null)
                nameValueCollection = new NameValueCollection();

            statusCode = 200;
            IMVCGridDefinition grid = MVCGridDefinitionTable.GetDefinitionInterface(gridName);
            QueryOptions options = QueryStringParser.ParseOptions(grid, nameValueCollection);
            GridContext gridContext = MVCGrid.NetCore.Utility.GridContextUtility.Create(/*context, */gridName, grid, options);

            GridEngine engine = new GridEngine();
            if (!engine.CheckAuthorization(gridContext))
            {
                //Forbidden
                statusCode = 403;
                return string.Empty;
            }

            IMVCGridRenderingEngine renderingEngine = GridEngine.GetRenderingEngine(gridContext);

            // TODO: Reimplement this for csv exports and other rendering responses.
            //renderingEngine.PrepareResponse(context.Response);

            StringBuilder sb = new StringBuilder();
            TextWriter htmlWriter = new StringWriter(sb);
            engine.Run(renderingEngine, gridContext, htmlWriter);
            string html = sb.ToString();
            return html;
        }
        internal static string GenerateGrid(string gridName)
        {
            int statusCode = 0;
            NameValueCollection nameValueCollection = new NameValueCollection();
            string html = GridHelpers.GenerateGrid(gridName, out statusCode, nameValueCollection);
            return html;
        }
    }
}
