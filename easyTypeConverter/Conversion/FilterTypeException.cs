using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion
{
    public class FilterTypeException : Exception
    {
        public FilterTypeException(object inData, Type inType, Filter filter, Exception? innerException = null)
            : base($@"invalid type {inType.Name} in filter  {filter.GetType().Name}", innerException)
        {

        }
    }
}
