using easyTypeConverter.Evaluating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering.Evaluators.Options
{
    public interface ITriggerOptions
    {
        Trigger Build(IEvaluatorContext evaluatorContext, ITriggerActionHandler actionHandler);
    }
}
