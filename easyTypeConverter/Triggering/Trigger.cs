using easyTypeConverter.Triggering.Action.Options;
using easyTypeConverter.Triggering.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering
{        
    public abstract class Trigger
    {
        readonly HashSet<Type> sourceTypes = new HashSet<Type>();
        readonly TriggerOptions options;
        readonly ITriggerActionHandler actionHandler;
        public Trigger(TriggerOptions options, ITriggerActionHandler actionHandler)
        {
            this.options = options;
            this.actionHandler = actionHandler;
        }
        protected bool IsSourceType(Type type) 
        { 
            if(sourceTypes.Count==0)
                return true; // If no source types defined, accept all types

            return sourceTypes.Contains(type); 
        }


        protected abstract bool OnEvaluate(TriggerInputContext? inputContext);
        public void Evaluate(TriggerInputContext? inputContext)
        {
            if(OnEvaluate(inputContext))
            {
                foreach (var action in options.Actions)
                {
                    if(this.actionHandler.Handle(inputContext, action) && options.ExitOnFirstMatch)
                    {
                        break;
                    }
                }

                if(options.DefaultAction!= null)
                {
                    this.actionHandler.Handle(inputContext, options.DefaultAction);
                }
            }
        }
    }
}
