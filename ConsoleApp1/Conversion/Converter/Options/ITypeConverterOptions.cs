using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter.Options
{
    public interface ITypeConverterOptions
    {
        List<IFilterOptions> InputFilters { get; }
        List<IFilterOptions> OutputFilters { get; }
        TypeConverter Build();
    }
}
