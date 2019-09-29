using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using MVCGrid.NetCore.SignalR.Models;
using System.Linq;

namespace MVCGrid.NetCore.SignalR
{
    public class MVCGridSignalR : Hub
    {
        public static ConcurrentDictionary<string, SignalRGridSession> SignalRGridSessions = new ConcurrentDictionary<string, SignalRGridSession>();

        public override async Task OnConnectedAsync()
        {
            string connectionId = this.Context.ConnectionId;
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = this.Context.ConnectionId;
            for (int x=0; SignalRGridSessions.Count > x; x++)
            {
                KeyValuePair<string, SignalRGridSession> session = SignalRGridSessions.ElementAt(x);
                session.Value.GridConnections.Remove(connectionId);
            }
        }
        public async Task Message(string gridName, string state, string html = "")
        {
            string connectionId = this.Context.ConnectionId;
            switch (state)
            {
                case "INIT":
                    {
                        SignalRGridSessions[gridName].GridConnections.Add(connectionId);
                        break;
                    }
                case "STOP":
                    {
                        
                        break;
                    }
                case "UPDATE":
                    {
                        
                        break;
                    }
            }
        }
    }
}
