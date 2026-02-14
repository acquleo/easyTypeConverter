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
        public void Analyze(ParamType paramType, string name, params object?[] args)
        {
            return;
        }

        public object? Evaluate(ParamType paramType, string name, params object?[] args)
        {
            if (args[0]?.ToString() == "A")
            {
                return 10;
            }
            if (args[0]?.ToString() == "B")
            {
                return 2;
            }
            return null;
        }
    }
}

