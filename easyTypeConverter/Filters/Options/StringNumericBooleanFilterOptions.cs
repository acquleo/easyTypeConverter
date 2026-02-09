using easyTypeConverter.Converter.Options;
using easyTypeConverter.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Filters.Options
{
    /// <summary>
    /// Effettua un replace esatto della stringa di input in una di output
    /// </summary>
    public class StringNumericBooleanFilterOptions : IStringNumericBooleanFilterOptions
    {
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public NumberStyles NumberStyle { get; set; } = NumberStyles.Any;
        public FilterAction Action { get; set; } = FilterAction.Continue;
        public Filter Build()
        {
            return new StringNumericBooleanFilter(this);
        }

    }
}
