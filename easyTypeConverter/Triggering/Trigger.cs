using easyTypeConverter.Evaluating;
using easyTypeConverter.Triggering.Action.Options;
using easyTypeConverter.Triggering.Evaluators.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering
{        
    public class Trigger
    {
        readonly HashSet<Type> sourceTypes = new HashSet<Type>();
        readonly TriggerOptions options;
        readonly ITriggerActionHandler actionHandler;
        readonly Evaluator? evaluator;
        public Trigger(TriggerOptions options,IEvaluatorContext evaluatorContext, ITriggerActionHandler actionHandler)
        {
            this.options = options;
            this.actionHandler = actionHandler;
            this.evaluator = options.Evaluator?.Build(evaluatorContext); //TODO: eccezione se l'evaluator è null

        }
        protected bool IsSourceType(Type type) 
        { 
            if(sourceTypes.Count==0)
                return true; // If no source types defined, accept all types

            return sourceTypes.Contains(type); 
        }

        public void Analyze()
        {
            this.evaluator?.Analyze();
        }

        public bool Evaluate()
        {
            var evaluationResult = Convert.ToBoolean(this.evaluator?.Evaluate());
            if (!evaluationResult)
                return false;

            foreach (var action in options.Actions)
            {
                if (this.actionHandler.Handle(action) && options.ExitOnFirstMatch)
                {
                    break;
                }
            }

            if (options.DefaultAction != null)
            {
                this.actionHandler.Handle(options.DefaultAction);
            }

            return evaluationResult;
        }
    }
}
