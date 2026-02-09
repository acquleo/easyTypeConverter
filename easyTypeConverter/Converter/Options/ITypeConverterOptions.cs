using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    public interface ITypeConverterOptions
    {
        List<IFilterOptions> InputFilters { get; }
        List<IFilterOptions> OutputFilters { get; }
        TypeConverter Build();
    }
}
