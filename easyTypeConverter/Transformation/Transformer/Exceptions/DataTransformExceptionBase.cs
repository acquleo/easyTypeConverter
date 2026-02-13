using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Exceptions
{
    public abstract class DataTransformExceptionBase: Exception
    {
        public DataTransformExceptionBase() { }

        public DataTransformExceptionBase(string message) : base(message) { }

        public DataTransformExceptionBase(string message, Exception? innerException) : base(message, innerException) { }
    }
}
