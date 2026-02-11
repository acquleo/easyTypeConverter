using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface ITimeSpanStringConverterOptions : ITypeConverterOptions
    {
        string? Format { get; set; }
        string Culture { get; set; }
    }
}
