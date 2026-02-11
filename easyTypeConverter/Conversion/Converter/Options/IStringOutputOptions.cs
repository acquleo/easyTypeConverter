using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public enum TextCase
    {
        Unchanged = 0,
        Lower,
        Upper,
    }

    public interface IStringOutputOptions : ITypeConverterOptions
    {
        public string Culture { get; set; }
        public TextCase Case { get; set; }
    }
}
