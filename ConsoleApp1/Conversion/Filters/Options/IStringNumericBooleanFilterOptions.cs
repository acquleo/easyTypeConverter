using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters.Options
{
    public interface IStringNumericBooleanFilterOptions:IFilterOptions
    {
        public CultureInfo Culture { get; set; }
        public NumberStyles NumberStyle { get; set; }

    }
}
