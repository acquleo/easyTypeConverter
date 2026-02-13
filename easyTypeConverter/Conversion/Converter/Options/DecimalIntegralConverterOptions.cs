using easyTypeConverter.Conversion.Filters.Options;
using System.Collections.Generic;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class DecimalIntegralConverterOptions : ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.DecimalIntegralConverter(this);
        }
    }
}
