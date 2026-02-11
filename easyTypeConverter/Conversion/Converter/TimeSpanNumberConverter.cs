using easyTypeConverter.Common;
using easyTypeConverter.Conversion.Converter.Options;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class TimeSpanNumberConverter : TypeConverter
    {
        TimeSpanNumberConverterOptions options;
        public TimeSpanNumberConverter(TimeSpanNumberConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public TimeSpanNumberConverter() : this(new TimeSpanNumberConverterOptions())
        {
        }

        public override List<DataType> SourceTypeList { get => new List<DataType>() { DataTypes.TimeSpan }; }
        public override List<DataType> TargetTypeList { get => new List<DataType>() { DataTypes.Double, DataTypes.Single, DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64, DataTypes.Decimal }; }

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;

            if (inData is not TimeSpan ts)
                return false;

            double value = options.Unit switch
            {
                TimeSpanNumberUnit.Ticks => ts.Ticks,
                TimeSpanNumberUnit.MicroSeconds => ts.TotalMicroseconds,
                TimeSpanNumberUnit.Milliseconds => ts.TotalMilliseconds,
                TimeSpanNumberUnit.Seconds => ts.TotalSeconds,
                TimeSpanNumberUnit.Minutes => ts.TotalMinutes,
                TimeSpanNumberUnit.Hours => ts.TotalHours,
                TimeSpanNumberUnit.Days => ts.TotalDays,
                _ => ts.TotalSeconds
            };

            outData = System.Convert.ChangeType(value, targetType.Type);
            return true;
        }
    }
}
