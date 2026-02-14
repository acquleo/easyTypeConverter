
using easyTypeConverter.Evaluating.Evaluators.Options;
using NCalc;
using NCalc.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluators
{
    public class StringEqualityEvaluator : Evaluator
    {
        readonly StringEqualityEvaluatorOptions options;
        public StringEqualityEvaluator(StringEqualityEvaluatorOptions options, IEvaluatorContext context) : base(options, context)
        {
            this.options = options;

        }

        public override void Analyze()
        {
            if (this.Context == null) return;

            try
            {
                this.Context.Analyze(ParamType.Param, options.Parameter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error analyzing expression: {ex.Message}", ex);
            }
        }

        public override object? Evaluate()
        {
            if(this.Context==null) return null;

            if (this.Context.Evaluate(ParamType.Param, options.Parameter)?.ToString() == options.Value)
            {
                return true;
            }

            return false;
        }
    }
}
