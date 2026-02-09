using easyTypeConverter.Filters;
using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    public class NumericConverterOptions :

        ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();

        public List<IFilterOptions> OutputFilters { get; set; } = new();

        public TypeConverter Build()
        {
            return new NumericConverter(this);
        }

    }
}
