using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;

namespace easyTypeConverter.Conversion.Converter
{
    public class DateTimeNumberConverter : TypeConverter
    {
        DateTimeNumberConverterOptions options;
        public DateTimeNumberConverter(DateTimeNumberConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public DateTimeNumberConverter() : this(new DateTimeNumberConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(DateTime) };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(double), typeof(float), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(decimal) }; 

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            if (inData is not DateTime dt)
                return false;
            double value = options.Unit switch
            {
                DateTimeNumberUnit.Ticks => dt.Ticks,
                DateTimeNumberUnit.Seconds => (dt - options.Epoch).TotalSeconds,
                DateTimeNumberUnit.Milliseconds => (dt - options.Epoch).TotalMilliseconds,
                DateTimeNumberUnit.Microseconds => (dt - options.Epoch).TotalMicroseconds,
                DateTimeNumberUnit.Minutes => (dt - options.Epoch).TotalMinutes,
                DateTimeNumberUnit.Hours => (dt - options.Epoch).TotalHours,
                DateTimeNumberUnit.Days => (dt - options.Epoch).TotalDays,
                DateTimeNumberUnit.OADate => dt.ToOADate(),
                _ => (dt - options.Epoch).TotalSeconds
            };
            outData = System.Convert.ChangeType(value, targetType);
            return true;
        }
    }
}
