using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion
{
    public class TargetTypeException : Exception
    {
        public TargetTypeException(TypeConverter converter, Exception? innerException = null)
            : base($@"target type not found in converter {converter.GetType().Name}", innerException)
        {

        }
    }
}
