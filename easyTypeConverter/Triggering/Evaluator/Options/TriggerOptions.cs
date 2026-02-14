using easyTypeConverter.Common;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation.Transformer.Options;
using easyTypeConverter.Triggering;
using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluator.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(EqualityTriggerOptions), "eq")]
    [PolymorphicDerivedType(typeof(NcalcExpressionTriggerOptions), "exp")]

    public abstract class TriggerOptions : IExtensibleOptions
    {
        [JsonPropertyName("actions")]
        public List<TriggerActionOptions> Actions { get; set; } = new List<TriggerActionOptions>();

        [JsonPropertyName("defaultAction")]
        public TriggerActionOptions? DefaultAction { get; set; }
        [JsonPropertyName("exitOnFirstMatch")]
        public bool ExitOnFirstMatch { get; set; } = false;
        public abstract Trigger Build(ITriggerActionHandler actionHandler);
    }
}
