using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Conversion.Converter.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class DecimalStringConverterOptions : IStringOutputOptions
    {
        [JsonPropertyName("format")]
        public string? Format { get; set; } = null;

        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;

        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        TextCase IStringOutputOptions.Case { get; set; } = TextCase.Unchanged;
        public TypeConverter Build()
        {
            return new Converter.DecimalStringConverter(this);
        }
    }
}
