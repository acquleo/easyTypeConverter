using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class BooleanStringConverterOptions : IStringOutputOptions
    {
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public TextCase Case { get; set; } = TextCase.Unchanged;

        public List<IFilterOptions> InputFilters { get; set; } = new();

        public List<IFilterOptions> OutputFilters { get; set; } = new();

        public TypeConverter Build()
        {
            return new BooleanStringConverter(this);
        }
    }
}
