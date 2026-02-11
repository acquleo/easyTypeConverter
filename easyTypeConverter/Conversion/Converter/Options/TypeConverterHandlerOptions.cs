using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class TypeConverterHandlerOptions
    {
        public List<ITypeConverterOptions> Converters { get; set; } = new();
        public TypeConverterHandler Build()
        {
            return new TypeConverterHandler(this);
        }
    }
}
