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
    public abstract class StringNumericConverterBaseOptions : ITypeConverterOptions, IStringIntegralConverterBaseOptions
    {
        [JsonPropertyName("style")]
        public NumberStyles NumberStyle { get; set; } = NumberStyles.Any;
        [JsonPropertyName("culture")]
        public string Culture { get; set; } = string.Empty;
        [JsonPropertyName("hexDetection")]
        public bool HexDetection { get; set; } = false;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();

        [JsonPropertyName("hexPrefixList")]
        public List<string> HexPrefixList { get; set; } = new() { "0x", "0X", "&h" };

        public abstract TypeConverter OnBuild();

        public TypeConverter Build()
        {
            return OnBuild();
        }
    }
}
