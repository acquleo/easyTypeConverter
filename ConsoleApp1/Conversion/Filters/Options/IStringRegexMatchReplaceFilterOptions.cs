using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters.Options
{
    public interface IStringRegexMatchReplaceFilterOptions : IFilterOptions
    {
        public List<(string, string)> Matches { get; internal set; }
    }
}
