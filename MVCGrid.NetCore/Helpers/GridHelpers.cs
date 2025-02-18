using MVCGrid.Interfaces;
using MVCGrid.Models;
using MVCGrid.NetCore.Engine;
using MVCGrid.NetCore.Interfaces;
using MVCGrid.Web;
using System.Collections.Specialized;
using System.IO;
using System.Text;

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
