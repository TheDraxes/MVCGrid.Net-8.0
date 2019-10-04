using System;
using System.Collections.Generic;
using System.Text;

namespace MVCGrid.NetCore.SignalR
{
    public class SignalRGridResponse
    {
        public string Type { get; set; }
        public string Gridname { get; set; }
        public string Html { get; set; }
        public string SummaryHtml { get; set; }
    }
}
