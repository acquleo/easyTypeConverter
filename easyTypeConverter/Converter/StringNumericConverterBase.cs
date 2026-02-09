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
    public abstract class StringNumericConverterBase : TypeConverter
    {
        readonly IStringNumericConverterBaseOptions options;
        public StringNumericConverterBase(IStringNumericConverterBaseOptions options) : base(options)
        {
            this.options = options;
        }

        public abstract bool OnNumericConvert(object inData, [NotNullWhen(true)] out object? outData, NumberStyles numberStyle);

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            if (this.options.HexDetection)
            {
                //auto detection HEX???
                if (inData is string strData)
                {
                    var match = this.options.HexPrefixList.FirstOrDefault(x => strData.StartsWith(x));

                    if (match != null)
                    {
                        return OnNumericConvert(strData.Substring(match.Length), out outData, NumberStyles.HexNumber);
                    }
                }
            }

            return OnNumericConvert(inData, out outData, options.NumberStyle);
        }

    }
}
