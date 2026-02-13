using easyTypeConverter.Evaluating.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluator.Options
{
    public class ExpressionEvaluatorOptions : DataEvaluatorOptions
    {
        [JsonPropertyName("expression")]
        public string Expression { get; set; } = string.Empty;
        public override DataEvaluator Build(IDataEvaluatorActionHandler actionHandler)
        {
            return new ExpressionDataEvaluator(this, actionHandler);
        }
    }
}
