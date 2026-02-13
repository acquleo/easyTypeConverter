using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Exceptions
{
    public class ConverterNotFoundException : TypeConvertExceptionBase
    {
        public ConverterNotFoundException(object inData, Type inType, Type outType, Exception? innerException = null)
            : base($@"cannot found converter of type {inType.Name} to type {outType.Name}", innerException)
        {

        }
    }
}
