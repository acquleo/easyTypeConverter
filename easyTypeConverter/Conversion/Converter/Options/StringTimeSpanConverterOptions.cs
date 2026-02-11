using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringTimeSpanConverterOptions : IStringTimeSpanConverterOptions
    {
        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;
        [JsonPropertyName("formats")]
        public string[]? Formats { get; set; } = null;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.StringTimeSpanConverter(this);
        }
    }
}
