using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System.Collections.Generic;

namespace easyTypeConverter.Conversion.Converters.Options
{
    public class BooleanNumberConverterOptions : ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converters.BooleanNumberConverter(this);
        }
    }
}
