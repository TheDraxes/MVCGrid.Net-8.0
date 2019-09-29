using MVCGrid.Models;
using MVCGrid.NetCore.SignalR;
using MVCGrid.NetCore.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCGrid.NetCore
{
    public class SignalRMVCGridBuilder : MVCGridBuilder<dynamic>
    {
        public SignalRMVCGridBuilder(string gridName, SignalRGridType signalRGridType) : base()
        {
            InitializeSignalR(gridName, signalRGridType);
        }

        public SignalRMVCGridBuilder(string gridName, SignalRGridType signalRGridType, GridDefaults gridDefaults) : base(gridDefaults, null)
        {
            InitializeSignalR(gridName, signalRGridType);
        }

        public SignalRMVCGridBuilder(string gridName, SignalRGridType signalRGridType, ColumnDefaults columnDefaults) : base(null, columnDefaults)
        {
            InitializeSignalR(gridName, signalRGridType);
        }

        /// <summary>
        /// Indicates that this grid is a SignalR MVCGrid.
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns></returns>
        public void InitializeSignalR(string gridName, SignalRGridType signalRGridType)
        {
            MVCGridSignalR.SignalRGridSessions.TryAdd(gridName, new SignalRGridSession(gridName));
            this.WithRetrieveDataMethod((context) =>
            {
                QueryOptions queryOptions = context.QueryOptions;
                int pageIndex = queryOptions.PageIndex.Value;
                int pageSize = queryOptions.ItemsPerPage.Value;
                int totalCount = 0;

                List<dynamic> data = MVCGridSignalR.SignalRGridSessions[context.GridName].Data.ToList();
                totalCount = data.Count;

                return new QueryResult<dynamic>()
                {
                    Items = data,
                    TotalRecords = totalCount
                };
            });
        }
    }
}
