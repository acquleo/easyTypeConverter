using easyTypeConverter.Evaluating.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluator
{
    public class EqualityDataEvaluator : DataEvaluator
    {
        readonly EqualityDataEvaluatorOptions options;
        public EqualityDataEvaluator(EqualityDataEvaluatorOptions options, IDataEvaluatorActionHandler actionHandler) : base(options, actionHandler)
        {
            this.options = options;
        }

        protected override bool OnEvaluate(DataEvaluatorInputContext? inputContext)
        {
            if(inputContext?.Value?.ToString() == options.ValueToCompare.ToString())
            {
                return true;
            }

            return false;
        }
    }
}
