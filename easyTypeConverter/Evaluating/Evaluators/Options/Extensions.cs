using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluators.Options
{
    public static class NcalcExpressionEvaluatorOptionsExtensions
    {
        public static T WithExpression<T>(this T obj, string expression )
                where T : NcalcExpressionEvaluatorOptions
        {
            obj.Expression = expression;
            return obj;
        }

    }

    public static class StringEqualityEvaluatorOptionsExtensions
    {
        public static T WithValue<T>(this T obj, string value)
                where T : StringEqualityEvaluatorOptions
        {
            obj.Value = value;
            return obj;
        }

        public static T WithParameter<T>(this T obj, string parameter)
                where T : StringEqualityEvaluatorOptions
        {
            obj.Parameter = parameter;
            return obj;

        }
    }
}
