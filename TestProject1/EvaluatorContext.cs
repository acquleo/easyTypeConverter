using easyTypeConverter.Evaluating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class EvaluatorContext : IEvaluatorContext
    {
        public void Analyze(string functionName, params object?[] args)
        {
            throw new NotImplementedException();
        }

        public object? Evaluate(string functionName, params object?[] args)
        {
            throw new NotImplementedException();
        }
    }
}
