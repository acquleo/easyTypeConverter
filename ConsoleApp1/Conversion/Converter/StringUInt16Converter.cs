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
    public class StringUInt16Converter : TypeConverter
    {
        StringByteConverterOptions options;
        public StringUInt16Converter(StringByteConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringUInt16Converter(): this(new StringByteConverterOptions())
        {
            
        }

        public override Type SourceType { get => typeof(string); }
        public override Type TargetType { get => typeof(UInt16); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = default;                      

            if (!UInt16.TryParse((string)inData, this.options.NumberStyle, 
                this.options.Culture, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
