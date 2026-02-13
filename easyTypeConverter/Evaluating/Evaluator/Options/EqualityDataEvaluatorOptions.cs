using easyTypeConverter.Evaluating.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluator.Options
{
    public class EqualityDataEvaluatorOptions : DataEvaluatorOptions
    {
        public string ValueToCompare { get; set; } = string.Empty;

        public override DataEvaluator Build(IDataEvaluatorActionHandler actionHandler)
        {
            return new EqualityDataEvaluator(this, actionHandler);
        }
    }
}
