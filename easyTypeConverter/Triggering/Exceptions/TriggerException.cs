using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Exceptions
{
    public class TriggerException : Exception
    {
        public TriggerException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}
