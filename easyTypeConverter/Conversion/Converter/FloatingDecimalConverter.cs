using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class FloatingDecimalConverter : TypeConverter
    {
        FloatingDecimalConverterOptions options;
        public FloatingDecimalConverter(FloatingDecimalConverterOptions options) : base(options)
        {
            this.options = options;
        }
        public FloatingDecimalConverter() : this(new FloatingDecimalConverterOptions()) { }
        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Single, DataTypes.Double };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.Decimal };
        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = System.Convert.ChangeType(inData, targetType.Type);
            return true;
        }
    }
}
