using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringTextInputOptions 
    {
        public StringComparison Comparison { get; internal set; } 
    }
}
