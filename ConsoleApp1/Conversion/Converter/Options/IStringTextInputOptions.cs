using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter.Options
{
    public interface IStringTextInputOptions : ITypeConverterOptions
    {
        public StringComparison Comparison { get; internal set; } 
    }
}
