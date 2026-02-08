using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter.Options
{
    public class Int32StringConverterOptions : ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();

        public List<IFilterOptions> OutputFilters { get; set; } = new();

        public TypeConverter Build()
        {
            return new Int32StringConverter(this);
        }
    }
}
