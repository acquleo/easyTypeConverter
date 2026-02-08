using ConsoleApp1.Conversion.Converter.Options;
using ConsoleApp1.Conversion.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters.Options
{
    /// <summary>
    /// Effettua un replace esatto della stringa di input in una di output
    /// </summary>
    public class StringRegexMatchReplaceFilterOptions : IStringRegexMatchReplaceFilterOptions
    {
        public List<(string, string)> Matches { get; set; } = new();
        public FilterAction Action { get; set; } = FilterAction.Continue;
        public Filter Build()
        {
            return new StringRegexMatchReplaceFilter(this);
        }

    }
}
