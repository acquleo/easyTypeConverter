using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace easyTypeConverter.Conversion.Converter
{
    public class FloatingStringConverter : TypeConverter
    {
        FloatingStringConverterOptions options;
        public FloatingStringConverter(FloatingStringConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public FloatingStringConverter() : this(new FloatingStringConverterOptions())
        {
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(float), typeof(double) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(string) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = null;

            double doubleInData = System.Convert.ToDouble(inData);

            outData = doubleInData.ToString(options.Format, options.Culture);

            return true;
        }
    }
}
