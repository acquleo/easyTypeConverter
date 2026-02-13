using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringDateTimeConverterOptions : ITypeConverterOptions, IStringDateTimeConverterOptions
    {
        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;

        [JsonPropertyName("formats")]
        public string[]? Formats { get; set; } = null;
        public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.None;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.StringDateTimeConverter(this);
        }
    }
}
