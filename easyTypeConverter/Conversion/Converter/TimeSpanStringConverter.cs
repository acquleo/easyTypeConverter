using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class TimeSpanStringConverter : TypeConverter
    {
        TimeSpanStringConverterOptions options;
        public TimeSpanStringConverter(TimeSpanStringConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public TimeSpanStringConverter() : this(new TimeSpanStringConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(TimeSpan) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(string) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            if (inData is not TimeSpan ts)
                return false;
            if (!string.IsNullOrEmpty(options.Format))
                outData = ts.ToString(options.Format, options.Culture);
            else
                outData = ts.ToString(options.Culture);
            return true;
        }
    }
}
