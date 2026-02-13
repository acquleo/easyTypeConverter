using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Exceptions
{
    public class DataTransformException : DataTransformExceptionBase
    {
        public DataTransformException(DataTransformer transformer, object inData, Type inType, Exception? innerException = null)
            : base($@"cannot transform data {inData} of type {inType.Name} using transformer {transformer.GetType().Name}", innerException)
        {

        }
    }
}
