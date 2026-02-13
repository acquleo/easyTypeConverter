using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class FloatingStringConverterOptions : ITypeConverterOptions, IStringOutputOptions
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
            return new Converter.FloatingStringConverter(this);
        }
    }
}
