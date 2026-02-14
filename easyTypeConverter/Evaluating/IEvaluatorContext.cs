using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating
{
    public interface IEvaluatorContext
    {
        public object? Evaluate(string functionName, params object?[] args);
        public void Analyze(string functionName, params object?[] args);
    }
}
