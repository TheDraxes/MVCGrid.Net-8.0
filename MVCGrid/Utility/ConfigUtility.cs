using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVCGrid.Utility
{
    internal class ConfigUtility
    {
        internal const string ShowErrorsAppSettingName = "MVCGridShowErrorDetail";

        [Obsolete("Will be removed soon due to compatability issues.")]
        internal static T GetAppSetting<T>(string name, T defaultValue)
        {
            return defaultValue;

            //string val = ConfigurationManager.AppSettings[name];

            //if (String.IsNullOrWhiteSpace(val))
            //{
            //    return defaultValue;
            //}

            //var converter = TypeDescriptor.GetConverter(typeof(T));
            //var result = converter.ConvertFrom(val);

            //return (T)result;
        }

        internal static bool GetShowErrorDetailsSetting()
        {
            return GetAppSetting<bool>(ShowErrorsAppSettingName, false);
        }
    }
}
