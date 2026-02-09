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
    public class StringUInt32Converter : TypeConverter
    {
        StringByteConverterOptions options;
        public StringUInt32Converter(StringByteConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringUInt32Converter(): this(new StringByteConverterOptions())
        {
            
        }

        public override Type SourceType { get => typeof(string); }
        public override Type TargetType { get => typeof(UInt32); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = default;                      

            if (!UInt32.TryParse((string)inData, this.options.NumberStyle, 
                this.options.Culture, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
