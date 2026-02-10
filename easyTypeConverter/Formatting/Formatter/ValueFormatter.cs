using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter
{
    /// <summary>
    /// base class for a value formatter
    /// </summary>
    public abstract class ValueFormatter
    {
        protected abstract bool OnFormat(FormatterContext inData, [NotNullWhen(true)] out string? outData);

        /// <summary>
        /// format the input data context
        /// </summary>
        /// <param name="inData">data context</param>
        /// <param name="outData">ouput formatted string</param>
        /// <returns></returns>
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
