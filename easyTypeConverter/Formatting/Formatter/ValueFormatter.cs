using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter
{
    public abstract class ValueFormatter
    {
        protected abstract bool OnFormat(FormatterContext inData, [NotNullWhen(true)] out string? outData);
        public bool Format(FormatterContext? inData, out string? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }


            return OnFormat(inData, out outData);
        }
    }
}
