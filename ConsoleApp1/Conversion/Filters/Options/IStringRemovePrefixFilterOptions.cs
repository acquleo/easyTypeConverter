using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters.Options
{
    public interface IStringRemovePrefixFilterOptions : IFilterOptions
    {
        public string Prefix { get; internal set; }
        public StringComparison Comparison { get; internal set; }
        
    }
}
