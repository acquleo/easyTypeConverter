using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class NumberBooleanConverter : TypeConverter
    {
        public NumberBooleanConverter(NumberBooleanConverterOptions options) : base(options) { }
        public NumberBooleanConverter() : this(new NumberBooleanConverterOptions()) { }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Byte, DataTypes.SByte, DataTypes.Int16, DataTypes.UInt16, DataTypes.Int32, DataTypes.UInt32, DataTypes.Int64, DataTypes.UInt64, DataTypes.Single, DataTypes.Double, DataTypes.Decimal };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.Boolean };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            double value = System.Convert.ToDouble(inData);
            const double epsilon = 1e-10;
            outData = Math.Abs(value) > epsilon;
            return true;
        }
    }
}
