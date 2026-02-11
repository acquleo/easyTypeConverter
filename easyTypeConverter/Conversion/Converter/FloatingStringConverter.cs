using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class FloatingStringConverter : TypeConverter
    {
        FloatingStringConverterOptions options;
        public FloatingStringConverter(FloatingStringConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public FloatingStringConverter() : this(new FloatingStringConverterOptions())
        {
        }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Single, DataTypes.Double };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.String };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);

            outData = null;

            double doubleInData = System.Convert.ToDouble(inData);

            outData = doubleInData.ToString(options.Format, culture);

            return true;
        }
    }
}
