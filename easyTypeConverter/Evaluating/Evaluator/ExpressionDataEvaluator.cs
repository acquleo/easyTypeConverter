using easyTypeConverter.Evaluating.Evaluator.Options;
using NCalc;
using NCalc.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluator
{
    public class ExpressionDataEvaluator : DataEvaluator
    {
        readonly ExpressionEvaluatorOptions options;
        readonly Expression expression;
        DataEvaluatorInputContext? inputContext;
        public ExpressionDataEvaluator(ExpressionEvaluatorOptions options, IDataEvaluatorActionHandler actionHandler) : base(options, actionHandler)
        {
            this.options = options;

            //utilizza regex per sostituire gli allarmi utilizzati sull'espressione con la chiamata alla funzione alm(alarmId) che punta al metodo GetValue            
            expression = new NCalc.Expression(options.Expression);
            expression.EvaluateFunction += Expression_EvaluateFunction;
            expression.EvaluateParameter += Expression_EvaluateParameter;
        }

        private void Expression_EvaluateParameter(string name, ParameterArgs args)
        {
            args.Result = name;
        }

        private void Expression_EvaluateFunction(string name, FunctionArgs args)
        {
            if(name == "sts")
            {
                args.Result = inputContext?.Status;
                return;
            }

            if(name == "val")
            {
                args.Result = inputContext?.Value;
                return;
            }

            if (name == "null")
            {
                args.Result = null;
                return;
            }
        }

        protected override bool OnEvaluate(DataEvaluatorInputContext? inputContext)
        {
            this.inputContext = inputContext ?? new DataEvaluatorInputContext() { Value = null, Status = null };
            
            var result = expression.Evaluate();

            this.inputContext = null;

            if (result is bool b)
            {
                return b;
            }

            return false;
        }
    }
}
