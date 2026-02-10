using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter
{
    public class IntegralConverter : TypeConverter
    {
        IntegralConverterOptions options;
        public IntegralConverter(IntegralConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public IntegralConverter(): this(new IntegralConverterOptions())
        {
            
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {            
            outData = System.Convert.ChangeType(inData, targetType);
            return true;
        }


    }
}
