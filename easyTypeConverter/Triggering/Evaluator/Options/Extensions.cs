using easyTypeConverter.Evaluating.Evaluators.Options;
using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluators.Options
{
    public static class TriggerOptionsExtensions
    {
        public static T WithAction<T>(this T obj, TriggerActionOptions action)
                where T : TriggerOptions
        {
            obj.Actions.Add(action);
            return obj;
        }

        public static T WithDefaultAction<T>(this T obj, TriggerActionOptions action)
                where T : TriggerOptions
        {
            obj.DefaultAction = action;
            return obj;
        }

        public static T WithEvaluator<T>(this T obj, EvaluatorOptions evaluator)
                where T : TriggerOptions
        {
            obj.Evaluator = evaluator;
            return obj;

        }
    }

}
