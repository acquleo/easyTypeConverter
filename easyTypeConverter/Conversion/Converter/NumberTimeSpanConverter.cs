using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class NumberTimeSpanConverter : TypeConverter
    {
        NumberTimeSpanConverterOptions options;
        public NumberTimeSpanConverter(NumberTimeSpanConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public NumberTimeSpanConverter() : this(new NumberTimeSpanConverterOptions())
        {
        }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Double, DataTypes.Single, DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64, DataTypes.Decimal }; 
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.TimeSpan };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            double value = System.Convert.ToDouble(inData);
            TimeSpan ts = options.Unit switch
            {
                TimeSpanNumberUnit.Ticks => TimeSpan.FromTicks((long)value),
                TimeSpanNumberUnit.MicroSeconds => TimeSpan.FromMicroseconds(value),
                TimeSpanNumberUnit.Milliseconds => TimeSpan.FromMilliseconds(value),
                TimeSpanNumberUnit.Seconds => TimeSpan.FromSeconds(value),
                TimeSpanNumberUnit.Minutes => TimeSpan.FromMinutes(value),
                TimeSpanNumberUnit.Hours => TimeSpan.FromHours(value),
                TimeSpanNumberUnit.Days => TimeSpan.FromDays(value),
                _ => TimeSpan.FromSeconds(value)
            };
            outData = ts;
            return true;
        }
    }
}
