using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;
using System.Text.Json.Serialization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class TimeSpanStringConverterOptions : ITimeSpanStringConverterOptions
    {
        [JsonPropertyName("format")]
        public string? Format { get; set; } = null;
        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.TimeSpanStringConverter(this);
        }
    }
}
