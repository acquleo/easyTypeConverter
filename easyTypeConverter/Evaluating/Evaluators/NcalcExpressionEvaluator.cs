
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
    public class NcalcExpressionEvaluator : Evaluator
    {
        readonly NcalcExpressionEvaluatorOptions options;
        readonly Expression expression;
        bool analyze = false;
        public NcalcExpressionEvaluator(NcalcExpressionEvaluatorOptions options, IEvaluatorContext context) : base(options, context)
        {
            this.options = options;

            //utilizza regex per sostituire gli allarmi utilizzati sull'espressione con la chiamata alla funzione alm(alarmId) che punta al metodo GetValue            
            expression = new NCalc.Expression(options.Expression);
            expression.EvaluateFunction += Expression_EvaluateFunction;
            expression.EvaluateParameter += Expression_EvaluateParameter;
        }

        private void Expression_EvaluateParameter(string name, ParameterArgs args)
        {
            if (this.Context == null)
            {
                return;
            }

            var function = name;

            if (analyze)
            {
                this.Context.Analyze(ParamType.Param, function);
                args.Result = null;
                return;
            }

            args.Result = this.Context.Evaluate(ParamType.Param, function);
        }

        private void Expression_EvaluateFunction(string name, FunctionArgs args)
        {
            if(this.Context==null)
            {
                return;
            }

            var function = name;
            var parameters = args.Parameters.Select(p => p.Evaluate()).ToArray();

            if(analyze)
            {
                this.Context.Analyze(ParamType.Function, function, parameters);
                args.Result = null;
                return;
            }

            args.Result = this.Context.Evaluate(ParamType.Function, function, parameters);
        }

        public override void Analyze()
        {
            try
            {
                analyze = true;
                expression.Evaluate();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error analyzing expression: {ex.Message}", ex);
            }
            finally { analyze = false; }
        }

        public override object? Evaluate()
        {
            try
            {
                var result = expression.Evaluate();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error analyzing expression: {ex.Message}", ex);
            }            
        }
    }
}
