using easyTypeConverter.Triggering.Action.Options;
using easyTypeConverter.Triggering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluator.Options
{
    public class NcalcExpressionTriggerOptions : TriggerOptions
    {
        [JsonPropertyName("expression")]
        public string Expression { get; set; } = string.Empty;

        public override Trigger Build(ITriggerActionHandler actionHandler)
        {
            return new NcalcExpressionDataEvaluator(this, actionHandler);
        }
    }
}
