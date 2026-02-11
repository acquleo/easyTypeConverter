using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Common
{
    public static class CultureInfoHelper
    {
        public static System.Globalization.CultureInfo GetCultureInfo(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                return System.Globalization.CultureInfo.InvariantCulture;
            }
            else
            {
                try
                {
                    return new System.Globalization.CultureInfo(culture);
                }
                catch
                {
                    return System.Globalization.CultureInfo.InvariantCulture;
                }
            }
        }
    }
}
