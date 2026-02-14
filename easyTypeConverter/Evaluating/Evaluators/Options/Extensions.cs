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
}
