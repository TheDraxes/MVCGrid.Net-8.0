using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCGrid.Extensions
{
    public static class MVCGridExtensions
    {
        public static UrlHelper NewUrlHelper()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                var request = new HttpRequest("/", "http://example.com", "");
                var response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }
            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);
            return new UrlHelper(requestContext);
        }
    }
}
