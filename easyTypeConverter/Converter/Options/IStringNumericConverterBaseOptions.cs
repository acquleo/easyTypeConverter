using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    public interface IStringNumericConverterBaseOptions : ITypeConverterOptions
    {
            NumberStyles NumberStyle { get; set; }
            CultureInfo Culture { get; set; }
            bool HexDetection { get; set; }
            List<string> HexPrefixList { get; set; }

    }
}
