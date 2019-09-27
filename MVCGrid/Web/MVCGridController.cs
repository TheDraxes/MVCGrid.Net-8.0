using MVCGrid.Engine;
using MVCGrid.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCGrid.Models;
using System.Web.Mvc;
using MVCGrid.Interfaces;
using System.Web;
using System.Threading.Tasks;

namespace MVCGrid.Web
{
    public class MVCGridController : Controller
    {
        public ActionResult Grid()
        {
            HttpContext context = System.Web.HttpContext.Current;
            string gridName = context.Request.QueryString["Name"];
            IMVCGridDefinition grid = MVCGridDefinitionTable.GetDefinitionInterface(gridName);
            QueryOptions options = QueryStringParser.ParseOptions(grid, context.Request);
            GridContext gridContext = GridContextUtility.Create(context, gridName, grid, options);

            GridEngine engine = new GridEngine();
            if (!engine.CheckAuthorization(gridContext))
            {
                return new HttpStatusCodeResult(403, "Access denied");
            }

            var renderingModel = engine.GenerateModel(gridContext);
            return PartialView(grid.ViewPath, renderingModel);
        }
    }

}
