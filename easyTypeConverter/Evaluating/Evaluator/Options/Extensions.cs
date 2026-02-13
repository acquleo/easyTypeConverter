using easyTypeConverter.Conversion;
using easyTypeConverter.Evaluating.Action.Options;
using easyTypeConverter.Evaluating.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Filters.Options
{
    public static class DataEvaluatorOptionsExtensions
    {
        public static T WithAction<T>(this T obj, DataEvaluatorActionOptions action)
                where T : DataEvaluatorOptions
        {
            obj.Actions.Add(action);
            return obj;
        }

    }

    public static class EqualityEvaluatorOptionsExtensions
    {
        public static T WithValueToCompare<T>(this T obj, string valueToCompare)
                where T : EqualityDataEvaluatorOptions
        {
            obj.ValueToCompare = valueToCompare;
            return obj;
        }

    }
}
