using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringTimeSpanConverterOptions 
    {
        string Culture { get; set; }
        string[]? Formats { get; set; }
    }
}
