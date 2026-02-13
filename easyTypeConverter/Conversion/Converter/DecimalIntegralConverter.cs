using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class DecimalIntegralConverter : TypeConverter
    {
        DecimalIntegralConverterOptions options;
        public DecimalIntegralConverter(DecimalIntegralConverterOptions options) : base(options)
        {
            this.options = options;
        }
        public DecimalIntegralConverter() : this(new DecimalIntegralConverterOptions()) { }
        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Decimal };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64 };
        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = System.Convert.ChangeType(inData, targetType.Type);
            return true;
        }
    }
}
