using Microsoft.AspNetCore.SignalR;
using MVCGrid.NetCore.Helpers;
using MVCGrid.NetCore.SignalR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCGrid.NetCore.SignalR.Helpers
{
    public class SignalRHelper
    {
        private static IHubContext<MVCGridSignalR> _hubContext;
        public static void Configure(IHubContext<MVCGridSignalR> hubContext)
        {
            _hubContext = hubContext;
        }

        public static async Task SendGridData(string gridName, GridGenerationType gridGenerationType)
        {
            SignalRGridSession session = MVCGridSignalR.SignalRGridSessions[gridName];
            List<string> connectionIds = session.GridConnections;

            SignalRGridResponse signalrGridResponse = SignalRGridHelpers.GenerateSignalRGrid(gridName, gridGenerationType);
            string json = JsonConvert.SerializeObject(signalrGridResponse);
            await _hubContext.Clients.Clients(connectionIds).SendCoreAsync("Message", new object[] { json });
        }
    }
}
