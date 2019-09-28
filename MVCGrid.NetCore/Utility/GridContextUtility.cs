using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using MVCGrid.Interfaces;
using MVCGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVCGrid.NetCore.Utility
{
    public partial class GridContextUtility
    {
        internal static GridContext Create(/*HttpContext context, */string gridName, IMVCGridDefinition grid, QueryOptions options)
        {
            //var httpContext = new HttpContextWrapper(context);
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
