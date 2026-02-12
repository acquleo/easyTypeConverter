using System;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class NumberDateTimeConverterOptions : INumberDateTimeConverterOptions
    {
        public NumberDateTimeUnit Unit { get; set; } = NumberDateTimeUnit.Seconds;
        public DateTime Epoch { get; set; } = DateTime.UnixEpoch;
        public DateTimeType Kind { get; set; } = DateTimeType.Unspecified;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.NumberDateTimeConverter(this);
        }
    }
}
