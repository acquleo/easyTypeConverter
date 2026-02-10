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

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(double), typeof(float), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(decimal) }; 
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(TimeSpan) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
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
