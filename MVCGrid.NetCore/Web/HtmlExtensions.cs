﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVCGrid.Engine;
using MVCGrid.Interfaces;
using MVCGrid.NetCore.Helpers;
using MVCGrid.Web;
using System;
using System.Collections.Generic;
using System.Text;

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

            string html = ge.GetBasePageHtml(/*helper, */HttpHelper.HttpContext.Request.ToNameValueCollection(), name, grid, pageParameters);

            return new HtmlString(html);
        }
    }
}
