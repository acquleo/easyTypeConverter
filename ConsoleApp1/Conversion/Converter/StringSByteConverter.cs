using ConsoleApp1.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter
{
    public class StringSByteConverter : TypeConverter
    {
        StringByteConverterOptions options;
        public StringSByteConverter(StringByteConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringSByteConverter(): this(new StringByteConverterOptions())
        {
            
        }

        public override Type SourceType { get => typeof(string); }
        public override Type TargetType { get => typeof(sbyte); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = default;                      

            if (!sbyte.TryParse((string)inData, this.options.NumberStyle, 
                this.options.Culture, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
