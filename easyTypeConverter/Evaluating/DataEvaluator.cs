using easyTypeConverter.Evaluating.Action.Options;
using easyTypeConverter.Evaluating.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating
{        
    public abstract class DataEvaluator
    {
        readonly HashSet<Type> sourceTypes = new HashSet<Type>();
        readonly DataEvaluatorOptions options;
        readonly IDataEvaluatorActionHandler actionHandler;
        public DataEvaluator(DataEvaluatorOptions options, IDataEvaluatorActionHandler actionHandler)
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


        protected abstract bool OnEvaluate(DataEvaluatorInputContext? inputContext);
        public void Evaluate(DataEvaluatorInputContext? inputContext)
        {
            if(OnEvaluate(inputContext))
            {
                foreach (var action in options.Actions)
                {
                    this.actionHandler.Handle(inputContext, action);
                }
            }
        }
    }
}
