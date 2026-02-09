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
    public class StringByteConverter : StringNumericConverterBase
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


        public override bool OnNumericConvert(object inData, [NotNullWhen(true)] out object? outData, NumberStyles numberStyle)
        {
            outData = default;

            if (!byte.TryParse((string)inData, numberStyle, this.options.Culture, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
