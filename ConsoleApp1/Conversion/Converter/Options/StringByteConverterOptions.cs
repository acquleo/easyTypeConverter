using ConsoleApp1.Conversion.Filters;
using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter.Options
{
    public class StringByteConverterOptions :
        ITypeConverterOptions
    {
        public NumberStyles NumberStyle { get; set; } = NumberStyles.None;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new StringByteConverter(this);
        }
    }
}
