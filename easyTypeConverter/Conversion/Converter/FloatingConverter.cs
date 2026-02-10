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
    public class FloatingConverter : TypeConverter
    {
        FloatingConverterOptions options;
        public FloatingConverter(FloatingConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public FloatingConverter(): this(new FloatingConverterOptions())
        {
            
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(float), typeof(double) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(float), typeof(double) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = System.Convert.ChangeType(inData, targetType);
            return true;
        }
    }
}
 