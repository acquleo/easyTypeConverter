using easyTypeConverter.Formatting.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter.Options
{
    public interface IValueFormatterOptions
    {
        ValueFormatter Build();
    }
}
