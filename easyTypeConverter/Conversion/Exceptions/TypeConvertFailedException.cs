using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Exceptions
{
    public class TypeConvertFailedException : TypeConvertExceptionBase
    {
        public TypeConvertFailedException(object inData, Type inType, Type outType, Exception? innerException = null)
            : base($@"failed to convert {inData} of type {inType.Name} to type {outType.Name}", innerException)
        {

        }
    }
}
