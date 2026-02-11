using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class BooleanNumberConverter : TypeConverter
    {
        public BooleanNumberConverter(BooleanNumberConverterOptions options) : base(options) { }
        public BooleanNumberConverter() : this(new BooleanNumberConverterOptions()) { }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Boolean };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { 
            DataTypes.Byte, DataTypes.SByte, DataTypes.Int16, DataTypes.UInt16, DataTypes.Int32, DataTypes.UInt32, DataTypes.Int64, DataTypes.UInt64, DataTypes.Single, DataTypes.Double, DataTypes.Decimal };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            if (inData is not bool b)
                return false;
            var value = b ? 1 : 0;
            outData = System.Convert.ChangeType(value, targetType.Type);
            return true;
        }
    }
}
