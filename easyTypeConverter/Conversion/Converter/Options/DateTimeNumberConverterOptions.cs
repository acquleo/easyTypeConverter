using System;
using System.Collections.Generic;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    

    public class DateTimeNumberConverterOptions : ITypeConverterOptions, IDateTimeNumberConverterOptions
    {
        public DateTimeNumberUnit Unit { get; set; } = DateTimeNumberUnit.Seconds;
        public DateTime Epoch { get; set; } = DateTime.UnixEpoch;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.DateTimeNumberConverter(this);
        }
    }
}
