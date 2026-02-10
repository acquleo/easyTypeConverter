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
    public class NumericConverter : TypeConverter
    {
        NumericConverterOptions options;
        public NumericConverter(NumericConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public NumericConverter(): this(new NumericConverterOptions())
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
