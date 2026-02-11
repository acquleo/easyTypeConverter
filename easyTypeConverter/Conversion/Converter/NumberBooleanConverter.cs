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

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(bool) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            double value = System.Convert.ToDouble(inData);
            const double epsilon = 1e-10;
            outData = Math.Abs(value) > epsilon;
            return true;
        }
    }
}
