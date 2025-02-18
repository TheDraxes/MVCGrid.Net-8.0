using MVCGrid.Engine;
using MVCGrid.Interfaces;
using MVCGrid.Models;
using MVCGrid.Utility;
using System.Web;
using System.Web.Mvc;

namespace MVCGrid.Web
{
    public class MVCGridController : Controller
    {
        public ActionResult Grid()
        {
            HttpContext context = System.Web.HttpContext.Current;
            string gridName = context.Request.QueryString["Name"];
            IMVCGridDefinition grid = MVCGridDefinitionTable.GetDefinitionInterface(gridName);
            QueryOptions options = QueryStringParser.ParseOptions(grid, context.Request.QueryString);
            GridContext gridContext = GridContextUtility.Create(/*context, */gridName, grid, options);

            GridEngine engine = new GridEngine();
            if (!engine.CheckAuthorization(gridContext, context.User.Identity.IsAuthenticated))
            {
                return new HttpStatusCodeResult(403, "Access denied");
            }

            var renderingModel = engine.GenerateModel(gridContext);
            return PartialView(grid.ViewPath, renderingModel);
        }
    }

}
