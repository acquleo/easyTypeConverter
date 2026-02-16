using easyTypeConverter.Evaluating;
using easyTypeConverter.Triggering.Actions.Options;
using easyTypeConverter.Triggering.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering
{        
    public class TriggerHandler
    {
        readonly TriggerHandlerOptions options;
        readonly List<Trigger> triggers = new List<Trigger>();
        public TriggerHandler(TriggerHandlerOptions options, IEvaluatorContext evaluatorContext, ITriggerActionHandler actionHandler)
        {
            this.options = options;
            foreach(var triggerOptions in options.Triggers)
            {
                var trigger = triggerOptions.Build(evaluatorContext, actionHandler);
                triggers.Add(trigger);
            }

        }

        public void Analyze()
        {
            foreach(var trigger in triggers)
            { 
                trigger.Analyze(); 
            }
        }

        public void Evaluate()
        {
            foreach (var trigger in triggers)
            {
                trigger.Evaluate();
            }
        }
    }
}
