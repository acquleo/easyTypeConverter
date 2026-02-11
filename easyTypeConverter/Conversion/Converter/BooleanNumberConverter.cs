using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class BooleanNumberConverter : TypeConverter
    {
        public BooleanNumberConverter(BooleanNumberConverterOptions options) : base(options) { }
        public BooleanNumberConverter() : this(new BooleanNumberConverterOptions()) { }

        public override List<Type> SourceTypeList { get; } = new List<Type>() { typeof(bool) };
        public override List<Type> TargetTypeList { get; } = new List<Type>() { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) };

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;
            if (inData is not bool b)
                return false;
            var value = b ? 1 : 0;
            outData = System.Convert.ChangeType(value, targetType);
            return true;
        }
    }
}
