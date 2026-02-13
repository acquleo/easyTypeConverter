using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Exceptions
{
    public class TypeConvertException : TypeConvertExceptionBase
    {
        public TypeConvertException(object inData, Type inType, Type outType, Exception? innerException = null)
            : base($@"cannot convert {inData} of type {inType.Name} to type {outType.Name}", innerException)
        {

        }
    }
}
