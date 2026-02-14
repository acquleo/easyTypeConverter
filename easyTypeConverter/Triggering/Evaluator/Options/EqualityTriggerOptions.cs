using easyTypeConverter.Triggering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluator.Options
{
    public class EqualityTriggerOptions : TriggerOptions
    {
        public string ValueToCompare { get; set; } = string.Empty;

        public override Trigger Build(ITriggerActionHandler actionHandler)
        {
            return new EqualityDataEvaluator(this, actionHandler);
        }
    }
}
