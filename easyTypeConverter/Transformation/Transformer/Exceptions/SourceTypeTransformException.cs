using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Exceptions
{
    public class SourceTypeTransformException : DataTransformExceptionBase
    {
        public SourceTypeTransformException(DataTransformer transformer, Type inType, Exception? innerException = null)
            : base($@"cannot transform source type {inType.Name} using transformer {transformer.GetType().Name}", innerException)
        {

        }
    }
}
