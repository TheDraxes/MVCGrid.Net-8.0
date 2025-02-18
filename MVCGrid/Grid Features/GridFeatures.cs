using System.Collections.Generic;
using System.Reflection;

namespace MVCGrid.Grid_Features
{
    public class GridFeatures
    {
        public static List<T> UniversalSearch<T>(List<T> data, string searchTerm)
        {
            searchTerm = searchTerm.ToLower().Trim();
            List<T> filteredData = new List<T>();
            for (int x = 0; data.Count > x; x++)
            {
                T datum = data[x];
                foreach (PropertyInfo propertyInfo in datum.GetType().GetProperties())
                {
                    if (propertyInfo.CanRead)
                    {
                        object value = propertyInfo.GetValue(datum, null);
                        bool isPrimitive = value.GetType().IsPrimitive;
                        bool containsSearchTerm = false;
                        if (isPrimitive == true)
                        {
                            string valueAsString = value.ToString();
                            valueAsString = valueAsString.ToLower();
                            if (valueAsString == searchTerm)
                                containsSearchTerm = true;
                            else if (valueAsString.Contains(searchTerm))
                                containsSearchTerm = true;
                        }
                        if (containsSearchTerm == true)
                        {
                            filteredData.Add(datum);
                        }
                    }
                }
            }
            return filteredData;
        }
    }
}
