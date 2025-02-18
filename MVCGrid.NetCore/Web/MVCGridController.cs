using Microsoft.AspNetCore.Mvc;
using MVCGrid.Models;
using MVCGrid.NetCore.Engine;
using MVCGrid.NetCore.Helpers;
using MVCGrid.NetCore.Interfaces;
using MVCGrid.NetCore.Utility;

namespace MVCGrid.Web
{
    public class MVCGridController : Controller
    {
        public IActionResult Grid()
        {
            string gridName = HttpContext.Request.Query["Name"];
            IMVCGridDefinition grid = MVCGridDefinitionTable.GetDefinitionInterface(gridName);
            QueryOptions options = QueryStringParser.ParseOptions(grid, HttpHelper.HttpContext.Request.ToNameValueCollection());
            GridContext gridContext = GridContextUtility.Create(/*context, */gridName, grid, options);

            GridEngine engine = new GridEngine();
            if (!engine.CheckAuthorization(gridContext))
            {
                return new StatusCodeResult(403);
            }

            var renderingModel = engine.GenerateModel(gridContext);
            return PartialView(grid.ViewPath, renderingModel);
        }
    }

}
