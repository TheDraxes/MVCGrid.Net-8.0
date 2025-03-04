﻿using MVCGrid.Interfaces;
using MVCGrid.NetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCGrid.Models
{
    public class GridContext
    {
        public GridContext()
        {
            Items = new Dictionary<string, object>();
            Identifier = Guid.NewGuid().ToString();
        }
        public string Identifier { get; set; }
        public IMVCGridDefinition GridDefinition { get; set; }
        //public HttpContext CurrentHttpContext { get; set; }
        public QueryOptions QueryOptions { get; set; }
        public string GridName { get; set; }

        public IEnumerable<IMVCGridColumn> GetVisibleColumns()
        {
            List<IMVCGridColumn> visibleColumns = new List<IMVCGridColumn>();

            var gridColumns = this.GridDefinition.GetColumns();

            if (QueryOptions.ColumnVisibility == null || QueryOptions.ColumnVisibility.Count == 0)
            {
                foreach (var col in gridColumns)
                {
                    if (col.Visible)
                    {
                        visibleColumns.Add(col);
                    }
                }
            }
            else
            {
                foreach (var colVis in QueryOptions.ColumnVisibility)
                {
                    var gridColumn = gridColumns.SingleOrDefault(p => p.ColumnName == colVis.ColumnName);

                    if (colVis.Visible)
                    {
                        visibleColumns.Add(gridColumn);
                    }
                }
            }

            if (visibleColumns.Count == 0)
            {
                visibleColumns.Add(this.GridDefinition.GetColumns().ElementAt(0));
            }

            return visibleColumns;
        }

        /// <summary>
        /// Arbitrary settings for this context
        /// </summary>
        public Dictionary<string, object> Items { get; set; }
    }
}
