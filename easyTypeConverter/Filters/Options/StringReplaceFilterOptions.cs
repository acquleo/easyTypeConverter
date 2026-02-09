using easyTypeConverter.Converter.Options;
using easyTypeConverter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Filters.Options
{
    /// <summary>
    /// Effettua un replace esatto della stringa di input in una di output
    /// </summary>
    public class StringReplaceFilterOptions : IStringReplaceFilterOptions
    {
        public List<(string, string)> Matches { get; set; } = new();
        public StringComparison Comparison { get; set; } = StringComparison.InvariantCulture;
        public FilterAction Action { get; set; } = FilterAction.Continue;
        public Filter Build()
        {
            return new StringReplaceFilter(this);
        }

    }
}
