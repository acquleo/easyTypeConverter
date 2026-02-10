using System.Globalization;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class TimeSpanStringConverterOptions : ITimeSpanStringConverterOptions
    {
        public string? Format { get; set; } = null;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.TimeSpanStringConverter(this);
        }
    }
}
