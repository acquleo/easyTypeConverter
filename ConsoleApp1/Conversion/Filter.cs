using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion
{
    public enum FilterAction
    {
        Continue,
        Exit
    }

    public abstract class Filter
    {
        public abstract Type Type { get; }

        public abstract FilterAction OnProcess(object inData, out object outData);

        public FilterAction Process(object inData, out object outData) { 
            var inType = inData.GetType();
            if (inType != Type)
                throw new FilterTypeException(inData, inType, this);

            return OnProcess(inData,out outData);
        
        }
    }
}
