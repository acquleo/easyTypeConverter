using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating
{
    public enum ParamType
    {        
        Param,
        Function
    }
    public interface IEvaluatorContext
    {
        public object? Evaluate(ParamType paramType, string name, params object?[] args);
        public void Analyze(ParamType paramType, string name, params object?[] args);
    }
}
