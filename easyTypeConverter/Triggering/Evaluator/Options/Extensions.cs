using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluator.Options
{
    public static class DataEvaluatorOptionsExtensions
    {
        public static T WithAction<T>(this T obj, TriggerActionOptions action)
                where T : TriggerOptions
        {
            obj.Actions.Add(action);
            return obj;
        }

    }

    public static class EqualityEvaluatorOptionsExtensions
    {
        public static T WithValueToCompare<T>(this T obj, string valueToCompare)
                where T : EqualityTriggerOptions
        {
            obj.ValueToCompare = valueToCompare;
            return obj;
        }

    }

    public static class ExpressionEvaluatorOptionsExtensions
    {
        public static T WithExpression<T>(this T obj, string expression )
                where T : NcalcExpressionTriggerOptions
        {
            obj.Expression = expression;
            return obj;
        }

    }
}
