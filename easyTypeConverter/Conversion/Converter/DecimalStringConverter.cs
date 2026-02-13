using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace easyTypeConverter.Conversion.Converter
{
    public class DecimalStringConverter : TypeConverter
    {
        DecimalStringConverterOptions options;
        public DecimalStringConverter(DecimalStringConverterOptions options) : base(options)
        {
            this.options = options;
        }
        public DecimalStringConverter() : this(new DecimalStringConverterOptions()) { }
        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Decimal };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.String };
        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(options.Culture);
            outData = options.Format != null
                ? ((decimal)inData).ToString(options.Format, culture)
                : ((decimal)inData).ToString(culture);
            return true;
        }
    }
}
