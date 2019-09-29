using Microsoft.AspNetCore.Http;
using MVCGrid.NetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCGrid.NetCore.Utility
{
    public class HtmlNetCoreUtility
    {
        public static string GetRootUrl()
        {
            return "";
            HttpRequest httpRequest = HttpHelper.HttpContext.Request;
            string applicationPath = $"{httpRequest.Scheme}://{httpRequest.Host.Value}";
            return applicationPath;
        }
    }
}
