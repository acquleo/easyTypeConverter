using easyTypeConverter.Common;
using easyTypeConverter.Evaluating;
using easyTypeConverter.Evaluating.Evaluators.Options;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation.Transformers.Options;
using easyTypeConverter.Triggering.Actions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Options
{
    public class TriggerHandlerOptions
    {
        [JsonPropertyName("triggers")]
        public List<TriggerOptions> Triggers { get; set; } = new List<TriggerOptions>();

        public TriggerHandler Build(IEvaluatorContext evaluatorContext, ITriggerActionHandler actionHandler)
        {
            return new TriggerHandler(this, evaluatorContext, actionHandler);
        }
    }
}
