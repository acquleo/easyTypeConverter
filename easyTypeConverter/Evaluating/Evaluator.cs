using easyTypeConverter.Evaluating.Evaluators.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating
{
    public abstract class Evaluator
    {
        readonly EvaluatorOptions options;
        public Evaluator(EvaluatorOptions options, IEvaluatorContext? context)
        {
            this.options = options;
            this.Context = context;
        }
        
        protected IEvaluatorContext? Context { get; private set; }
        public abstract void Analyze();
        public abstract object? Evaluate();
    }
}
