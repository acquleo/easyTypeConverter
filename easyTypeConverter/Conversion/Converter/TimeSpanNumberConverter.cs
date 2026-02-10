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

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(TimeSpan) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(double), typeof(float), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(decimal) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
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

            outData = System.Convert.ChangeType(value, targetType);
            return true;
        }
    }
}
