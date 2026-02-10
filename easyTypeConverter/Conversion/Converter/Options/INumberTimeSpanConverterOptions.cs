using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface INumberTimeSpanConverterOptions : ITypeConverterOptions
    {
        TimeSpanNumberUnit Unit { get; set; }
    }
}
