using easyTypeConverter.Filters;
using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    public class StringSByteConverterOptions :
        StringNumericConverterBaseOptions
    {

        public override TypeConverter OnBuild()
        {
            return new StringSByteConverter(this);
        }
    }
}
