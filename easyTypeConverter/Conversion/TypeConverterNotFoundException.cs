using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion
{
    public class TypeConverterNotFoundException : Exception
    {
        public TypeConverterNotFoundException(object inData, Type inType, Type outType, Exception? innerException = null)
            : base($@"cannot found converter of type {inType.Name} to type {outType.Name}", innerException)
        {

        }
    }
}
