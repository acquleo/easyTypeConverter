using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringIntegralConverterBaseOptions : ITypeConverterOptions
    {
            NumberStyles NumberStyle { get; set; }
            CultureInfo Culture { get; set; }
            bool HexDetection { get; set; }
            List<string> HexPrefixList { get; set; }

    }
}
