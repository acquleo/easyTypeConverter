using System;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converters.Options
{
    public interface ITimeSpanNumberConverterOptions 
    {
        TimeSpanNumberUnit Unit { get; set; }
    }
}
