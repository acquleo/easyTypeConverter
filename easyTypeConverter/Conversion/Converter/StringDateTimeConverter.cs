using easyTypeConverter.Common;
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

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.String };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.DateTime };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);

            outData = null;
            var str = inData as string;
            if (str == null)
                return false;
            if (options.Formats != null && options.Formats.Length > 0)
            {
                if (!DateTime.TryParseExact(str, options.Formats, culture, options.DateTimeStyles, out var dt))
                    return false;
                outData = dt;
                return true;
            }
            else
            {
                if (!DateTime.TryParse(str, culture, options.DateTimeStyles, out var dt))
                    return false;
                outData = dt;
                return true;
            }
        }
    }
}
