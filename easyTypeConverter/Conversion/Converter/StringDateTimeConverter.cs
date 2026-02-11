using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace easyTypeConverter.Conversion.Converter
{
    public class StringDateTimeConverter : TypeConverter
    {
        StringDateTimeConverterOptions options;
        public StringDateTimeConverter(StringDateTimeConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public StringDateTimeConverter() : this(new StringDateTimeConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(string) };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(DateTime) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            var str = inData as string;
            if (str == null)
                return false;
            if (options.Formats != null && options.Formats.Length > 0)
            {
                if (!DateTime.TryParseExact(str, options.Formats, options.Culture, options.DateTimeStyles, out var dt))
                    return false;
                outData = dt;
                return true;
            }
            else
            {
                if (!DateTime.TryParse(str, options.Culture, options.DateTimeStyles, out var dt))
                    return false;
                outData = dt;
                return true;
            }
        }
    }
}
