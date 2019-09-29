using MVCGrid.Models;
using MVCGrid.NetCore;
using MVCGrid.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCGrid.NetCore.SignalR.Extensions
{
    public static class Conversions
    {
        public static SignalRMVCGridBuilder ToSignalRGrid(this MVCGridBuilder<dynamic> builder)
        {
            return (SignalRMVCGridBuilder)builder;
        }
    }
}
