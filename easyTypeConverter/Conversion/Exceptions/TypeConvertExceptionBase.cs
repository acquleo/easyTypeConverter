using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Exceptions
{
    public abstract class TypeConvertExceptionBase: Exception
    {
        public TypeConvertExceptionBase() { }

        public TypeConvertExceptionBase(string message) : base(message) { }

        public TypeConvertExceptionBase(string message, Exception? innerException) : base(message, innerException) { }
    }
}
