using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class BooleanStringConverterOptions : ITypeConverterOptions, IStringOutputOptions
    {
        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;

        [JsonPropertyName("case")]
        public TextCase Case { get; set; } = TextCase.Unchanged;

        [JsonIgnore]
        public List<IFilterOptions> InputFilters { get; set; } = new();
        [JsonIgnore]
        public List<IFilterOptions> OutputFilters { get; set; } = new();

        public TypeConverter Build()
        {
            return new BooleanStringConverter(this);
        }
    }
}
