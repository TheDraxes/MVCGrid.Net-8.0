using MVCGrid.Interfaces;
using MVCGrid.Models;

namespace MVCGrid.Utility
{
    public partial class GridContextUtility
    {
        internal static GridContext Create(/*HttpContext context, */string gridName, IMVCGridDefinition grid, QueryOptions options)
        {
            //var httpContext = new HttpContextWrapper(context);
            //var urlHelper = new System.Web.Mvc.UrlHelper(new RequestContext(httpContext, new RouteData()));

            var gridContext = new GridContext()
            {
                GridName = gridName,
                //CurrentHttpContext = context,
                GridDefinition = grid,
                QueryOptions = options,
            };

            return gridContext;
        }
    }
}
