using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter
{
    public class IntegralConverter : TypeConverter
    {
        IntegralConverterOptions options;
        public IntegralConverter(IntegralConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public IntegralConverter(): this(new IntegralConverterOptions())
        {
            
        }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64 };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64 };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {            
            outData = System.Convert.ChangeType(inData, targetType.Type);
            return true;
        }


    }
}
