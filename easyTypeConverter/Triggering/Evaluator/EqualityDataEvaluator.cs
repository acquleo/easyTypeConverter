using easyTypeConverter.Triggering;
using easyTypeConverter.Triggering.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluator
{
    public class EqualityDataEvaluator : Trigger
    {
        readonly EqualityTriggerOptions options;
        public EqualityDataEvaluator(EqualityTriggerOptions options, ITriggerActionHandler actionHandler) : base(options, actionHandler)
        {
            this.options = options;
        }

        
        protected override bool OnEvaluate(TriggerInputContext? inputContext)
        {
            if(inputContext?.Value?.ToString() == options.ValueToCompare.ToString())
            {
                return true;
            }

            return false;
        }
    }
}
