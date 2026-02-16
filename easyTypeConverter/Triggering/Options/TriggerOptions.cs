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
    public class TriggerOptions : ITriggerOptions
    {
        [JsonPropertyName("evaluator")]
        public EvaluatorOptions? Evaluator { get; set; }
        [JsonPropertyName("actions")]
        public List<TriggerActionOptions> Actions { get; set; } = new List<TriggerActionOptions>();

        [JsonPropertyName("defaultAction")]
        public TriggerActionOptions? DefaultAction { get; set; }
        [JsonPropertyName("exitOnFirstMatch")]
        public bool ExitOnFirstMatch { get; set; } = false;
        public Trigger Build(IEvaluatorContext evaluatorContext, ITriggerActionHandler actionHandler)
        {
            return new Trigger(this, evaluatorContext, actionHandler);
        }
    }
}
