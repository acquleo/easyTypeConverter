using easyTypeConverter.Triggering.Action.Options;
using easyTypeConverter.Triggering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using easyTypeConverter.Evaluating.Evaluators;

namespace easyTypeConverter.Evaluating.Evaluators.Options
{
    public class StringEqualityEvaluatorOptions : EvaluatorOptions
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
        [JsonPropertyName("parameter")]
        public string Parameter { get; set; } = string.Empty;

        public override Evaluator Build(IEvaluatorContext context)
        {
            return new StringEqualityEvaluator(this, context);
        }
    }
}
