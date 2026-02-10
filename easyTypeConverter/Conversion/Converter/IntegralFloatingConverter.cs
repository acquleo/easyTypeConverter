using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class IntegralFloatingConverter : TypeConverter
    {
        IntegralConverterOptions options;
        public IntegralFloatingConverter(IntegralConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public IntegralFloatingConverter() : this(new IntegralConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get; } = new List<Type>()
        {
            typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long)
        };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(float), typeof(double) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = System.Convert.ChangeType(inData, targetType);
            return true;
        }
    }
}
