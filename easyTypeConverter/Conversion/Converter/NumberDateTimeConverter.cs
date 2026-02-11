using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;

namespace easyTypeConverter.Conversion.Converter
{
    public class NumberDateTimeConverter : TypeConverter
    {
        NumberDateTimeConverterOptions options;
        public NumberDateTimeConverter(NumberDateTimeConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public NumberDateTimeConverter() : this(new NumberDateTimeConverterOptions())
        {
        }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Double, DataTypes.Single, DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64, DataTypes.Decimal };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() { DataTypes.DateTime };

        private static DateTimeKind ToDateTimeKind(SerializableDateTimeKind kind)
        {
            return kind switch
            {
                SerializableDateTimeKind.Utc => DateTimeKind.Utc,
                SerializableDateTimeKind.Local => DateTimeKind.Local,
                _ => DateTimeKind.Unspecified
            };
        }

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            double value = System.Convert.ToDouble(inData);

            DateTime result = options.Unit switch
            {
                NumberDateTimeUnit.Ticks => new DateTime((long)value, ToDateTimeKind(options.Kind)),
                NumberDateTimeUnit.Seconds => options.Epoch.AddSeconds(value),
                NumberDateTimeUnit.Milliseconds => options.Epoch.AddMilliseconds(value),
                NumberDateTimeUnit.Microseconds => options.Epoch.AddMicroseconds(value),
                NumberDateTimeUnit.Minutes => options.Epoch.AddMinutes(value),
                NumberDateTimeUnit.Hours => options.Epoch.AddHours(value),
                NumberDateTimeUnit.Days => options.Epoch.AddDays(value),
                NumberDateTimeUnit.OADate => DateTime.FromOADate(value),
                _ => options.Epoch.AddSeconds(value)
            };
            outData = result;
            return true;
        }
    }
}
