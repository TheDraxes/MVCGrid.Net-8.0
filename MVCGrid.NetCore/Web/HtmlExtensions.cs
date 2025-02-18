using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCGrid.NetCore.Engine;
using MVCGrid.NetCore.Interfaces;
using MVCGrid.Web;

namespace MVCGrid.NetCore.Web
{
    public static class HtmlExtensions
    {
        public static IHtmlContent MVCGridNetCore<TModel>(this IHtmlHelper<TModel> html, string name)
        {
            var currentMapping = MVCGridDefinitionTable.GetDefinitionInterface(name);

            return MVCGridNetCore(html, name, currentMapping, null);
        }

        public static IHtmlContent MVCGridNetCore<TModel>(this IHtmlHelper<TModel> html, string name, object pageParameters)
        {
            var currentMapping = MVCGridDefinitionTable.GetDefinitionInterface(name);

            return MVCGridNetCore(html, name, currentMapping, pageParameters);
        }

        internal static IHtmlContent MVCGridNetCore<TModel>(this IHtmlHelper<TModel> helper, string name, IMVCGridDefinition grid, object pageParameters)
        {
            GridEngine ge = new GridEngine();

            string html = ge.GetBasePageHtml(name, grid, pageParameters);

            return new HtmlString(html);
        }
    }
}
