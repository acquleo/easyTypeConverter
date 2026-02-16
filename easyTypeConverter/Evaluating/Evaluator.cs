using easyTypeConverter.Evaluating.Evaluators.Options;
using easyTypeConverter.Evaluating.Exceptions;
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
        public abstract void OnAnalyze();
        public void Analyze()
        {
            try
            {
                OnAnalyze();
            }
            catch (Exception ex)
            {
                throw new EvaluatorException("An error occurred during analyze.", ex);
            }
        }

        public abstract object? OnEvaluate();
        public object? Evaluate()
        {
            try
            {
                return OnEvaluate();
            }
            catch (Exception ex)
            {
                throw new EvaluatorException("An error occurred during evaluation.", ex);
            }
        }
    }
}
