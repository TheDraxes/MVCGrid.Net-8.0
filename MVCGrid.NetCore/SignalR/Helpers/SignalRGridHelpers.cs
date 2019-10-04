using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using MVCGrid.Web;
using MVCGrid.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MVCGrid.NetCore.Helpers;
using MVCGrid.Utility;
using MVCGrid.NetCore.Engine;
using MVCGrid.Interfaces;
using System.Collections.Specialized;
using HtmlAgilityPack;

namespace MVCGrid.NetCore.SignalR.Helpers
{
    public class SignalRGridHelpers
    {
        internal static SignalRGridResponse GenerateSignalRGrid(string gridName, GridGenerationType type)
        {
            string html = string.Empty;
            string summaryhtml = string.Empty;
            int statusCode = 0;
            NameValueCollection nameValueCollection = new NameValueCollection();
            html = GridHelpers.GenerateGrid(gridName, out statusCode, nameValueCollection);

            if (type == GridGenerationType.Row)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                List<HtmlNode> trNodes = doc.DocumentNode.Descendants("tr").ToList();
                HtmlNode trNode = trNodes.LastOrDefault();
                HtmlNode summaryNode = doc.GetElementbyId($"MVCGridTable_{gridName}_Summary");

                if (trNode != null)
                {
                    html = trNode.OuterHtml;
                    if (html.Contains("noresults") == true)
                        html = string.Empty;
                }
                if (summaryNode != null)
                {
                    summaryhtml = summaryNode.OuterHtml;
                }
            }

            return new SignalRGridResponse()
            {
                Type = Enum.GetName(typeof(GridGenerationType), type),
                Gridname = gridName,
                Html = html,
                SummaryHtml = summaryhtml,
            };
        }
    }
}
