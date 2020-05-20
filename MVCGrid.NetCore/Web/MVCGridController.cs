using MVCGrid.NetCore.Engine;
using MVCGrid.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCGrid.Models;
using MVCGrid.Interfaces;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCGrid.NetCore.Utility;
using MVCGrid.NetCore.Helpers;
using MVCGrid.NetCore.Interfaces;

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
