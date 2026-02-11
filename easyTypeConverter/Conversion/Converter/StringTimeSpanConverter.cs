using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace easyTypeConverter.Conversion.Converter
{
    public class StringTimeSpanConverter : TypeConverter
    {
        StringTimeSpanConverterOptions options;
        public StringTimeSpanConverter(StringTimeSpanConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public StringTimeSpanConverter() : this(new StringTimeSpanConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(string) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(TimeSpan) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);

            outData = default;
            var str = (string)inData;

            if (options.Formats != null && options.Formats.Length > 0)
            {
                if (!TimeSpan.TryParseExact(str, options.Formats, culture, out var ts))
                    return false;
                outData = ts;
                return true;
            }
            else
            {
                if (!TimeSpan.TryParse(str, culture, out var ts))
                    return false;
                outData = ts;
                return true;
            }
        }
    }
}
