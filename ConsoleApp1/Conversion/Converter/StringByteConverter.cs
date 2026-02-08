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
    public class StringByteConverter : TypeConverter
    {
        StringByteConverterOptions options;
        public StringByteConverter(StringByteConverterOptions options) : base(options)
        {
            this.options = options;
        }

        public StringByteConverter(): this(new StringByteConverterOptions())
        {
            
        }

        public override Type SourceType { get => typeof(string); }
        public override Type TargetType { get => typeof(byte); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = default;                      

            if (!byte.TryParse((string)inData, this.options.NumberStyle, this.options.Culture, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
