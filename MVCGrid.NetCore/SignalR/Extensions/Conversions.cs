using MVCGrid.Models;

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
