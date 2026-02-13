using easyTypeConverter.Evaluating;
using easyTypeConverter.Evaluating.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class ActionHandler : IDataEvaluatorActionHandler
    {
        public bool Handle(DataEvaluatorInputContext? inputContext, DataEvaluatorActionOptions options)
        {
            Console.WriteLine($@"handling {options.GetType().Name}");
            return true;
        }
    }

    public class SetStatusActionOptions : DataEvaluatorActionOptions
    {
        public string StatusToBeSet { get; set; } = string.Empty;

    }
}
