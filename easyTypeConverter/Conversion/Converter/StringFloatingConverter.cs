using easyTypeConverter.Common;
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
    public class StringFloatingConverter : TypeConverter
    {
        StringFloatingConverterOptions options;
        public StringFloatingConverter(StringFloatingConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringFloatingConverter(): this(new StringFloatingConverterOptions())
        {
            
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(string) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(float), typeof(double) }; }


        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);
            outData = default;

            if (!double.TryParse((string)inData, options.NumberStyle,
                culture, out var doubleParsed))
                return false;


            outData = System.Convert.ChangeType(doubleParsed, targetType); ;
            return true;
        }



    }
}
