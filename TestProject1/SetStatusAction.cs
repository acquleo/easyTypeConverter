using easyTypeConverter.Triggering;
using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class ActionHandler : ITriggerActionHandler
    {
        public bool Handle(TriggerInputContext? inputContext, TriggerActionOptions options)
        {
            Console.WriteLine($@"handling {options.GetType().Name}");
            return true;
        }
    }

    public class SetStatusActionOptions : TriggerActionOptions
    {
        public string StatusToBeSet { get; set; } = string.Empty;

    }
}
