using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(double), typeof(float), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(decimal) };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(DateTime) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            double value = System.Convert.ToDouble(inData);

            DateTime result = options.Unit switch
            {
                NumberDateTimeUnit.Ticks => new DateTime((long)value, options.Kind),
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
