using easyTypeConverter.Conversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringFloatingConverterOptions : StringNumericConverterBaseOptions
    {
        public override TypeConverter OnBuild()
        {
            return new StringFloatingConverter(this);
        }

    }
}
