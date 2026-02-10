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
    public abstract class StringIntegralConverterBase : TypeConverter
    {
        readonly IStringIntegralConverterBaseOptions options;
        public StringIntegralConverterBase(IStringIntegralConverterBaseOptions options) : base(options)
        {
            this.options = options;
        }

        public abstract bool OnNumericConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData, NumberStyles numberStyle);

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            if (this.options.HexDetection)
            {
                //auto detection HEX???
                if (inData is string strData)
                {
                    var match = this.options.HexPrefixList.FirstOrDefault(x => strData.StartsWith(x));

                    if (match != null)
                    {
                        return OnNumericConvert(strData.Substring(match.Length), targetType, out outData, NumberStyles.HexNumber);
                    }
                }
            }

            return OnNumericConvert(inData, targetType, out outData, options.NumberStyle);
        }

    }
}
