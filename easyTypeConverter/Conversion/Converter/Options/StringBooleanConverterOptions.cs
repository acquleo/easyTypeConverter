using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringBooleanConverterOptions : ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new StringBooleanConverter(this);
        }
    }
}
