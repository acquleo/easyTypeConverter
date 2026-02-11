using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class TimeSpanNumberConverterOptions : ITimeSpanNumberConverterOptions
    {
        [JsonPropertyName("unit")]
        public TimeSpanNumberUnit Unit { get; set; } = TimeSpanNumberUnit.Seconds;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.TimeSpanNumberConverter(this);
        }
    }
}
