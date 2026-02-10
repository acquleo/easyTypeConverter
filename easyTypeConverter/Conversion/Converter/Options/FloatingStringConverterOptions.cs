using System;
using System.Collections.Generic;
using System.Globalization;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class FloatingStringConverterOptions : IStringOutputOptions
    {
        public string? Format { get; set; } = null;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        public List<IFilterOptions> InputFilters { get; set; } = new();

        public List<IFilterOptions> OutputFilters { get; set; } = new();
        TextCase IStringOutputOptions.Case { get; set; } = TextCase.Unchanged;  

        public TypeConverter Build()
        {
            return new Converter.FloatingStringConverter(this);
        }
    }
}
