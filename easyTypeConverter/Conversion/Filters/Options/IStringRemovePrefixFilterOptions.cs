using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Filters.Options
{
    public interface IStringRemovePrefixFilterOptions 
    {
        public string Prefix { get; internal set; }
        public StringComparison Comparison { get; internal set; }
        
    }
}
