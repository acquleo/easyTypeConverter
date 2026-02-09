using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Filters.Options
{
    public interface IFilterOptions
    {
        FilterAction Action { get; set; }
        Filter Build();
    }
}
