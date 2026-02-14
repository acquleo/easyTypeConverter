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
    public class NcalcExpressionEvaluatorOptions : EvaluatorOptions
    {
        [JsonPropertyName("expression")]
        public string Expression { get; set; } = string.Empty;

        public override Evaluator Build(IEvaluatorContext context)
        {
            return new NcalcExpressionEvaluator(this, context);
        }
    }
}
