using System;
using System.Collections.Generic;
using System.Globalization;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringDateTimeConverterOptions : IStringDateTimeConverterOptions
    {
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public string[]? Formats { get; set; } = null;
        public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.None;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.StringDateTimeConverter(this);
        }
    }
}
