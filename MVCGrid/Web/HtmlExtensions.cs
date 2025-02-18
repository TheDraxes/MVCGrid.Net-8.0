using MVCGrid.Engine;
using MVCGrid.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace MVCGrid.Web
{
    public static class HtmlExtensions
    {
        public static IHtmlString MVCGrid(this HtmlHelper helper, string name)
        {
            var currentMapping = MVCGridDefinitionTable.GetDefinitionInterface(name);

            return MVCGrid(helper, name, currentMapping, null);
        }

        public static IHtmlString MVCGrid(this HtmlHelper helper, string name, object pageParameters)
        {
            var currentMapping = MVCGridDefinitionTable.GetDefinitionInterface(name);

            return MVCGrid(helper, name, currentMapping, pageParameters);
        }

        internal static IHtmlString MVCGrid(this HtmlHelper helper, string name, IMVCGridDefinition grid, object pageParameters)
        {
            GridEngine ge = new GridEngine();
            ControllerContext context = helper.ViewContext.Controller.ControllerContext;
            string html = ge.GetBasePageHtml(/*helper, */HttpContext.Current.Request.QueryString, name, grid, pageParameters);

            return MvcHtmlString.Create(html);
        }
    }
}
