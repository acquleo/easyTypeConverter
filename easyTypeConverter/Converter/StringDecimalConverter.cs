using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter
{
    public class StringDecimalConverter : TypeConverter
    {
        StringDecimalConverterOptions options;
        public StringDecimalConverter(StringDecimalConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringDecimalConverter(): this(new StringDecimalConverterOptions())
        {
            
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(string) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(decimal) }; }


        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = default;

            if (!decimal.TryParse((string)inData, options.NumberStyle,
                this.options.Culture, out var decimalParsed))
                return false;


            outData = decimalParsed;
            return true;
        }



    }
}
