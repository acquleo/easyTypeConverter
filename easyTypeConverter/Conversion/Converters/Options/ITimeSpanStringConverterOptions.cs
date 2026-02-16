using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converters.Options
{
    public interface ITimeSpanStringConverterOptions 
    {
        string? Format { get; set; }
        string Culture { get; set; }
    }
}
