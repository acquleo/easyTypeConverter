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
    /// Effettua un rtrim della stringa
    /// </summary>
    public class StringTrimFilterOptions : IStringTrimFilterOptions
    {
        public FilterAction Action { get; set; } = FilterAction.Continue;
        public Filter Build()
        {
            return new StringTrimFilter(this);
        }

    }
}
