using easyTypeConverter.Triggering;
using easyTypeConverter.Triggering.Actions.Options;
using easyTypeConverter.Triggering.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class ActionHandler : ITriggerActionHandler
    {
        public bool Handle(TriggerActionOptions options)
        {
            Console.WriteLine($@"handling {options.GetType().Name}");
            return true;
        }
    }

    public class SetStatusActionOptions : TriggerActionOptions
    {
        public string StatusToBeSet { get; set; } = string.Empty;

    }

    public static class SetStatusActionOptionsExtensions
    {
        public static T WithStatusToBeSet<T>(this T obj, string status)
                where T : SetStatusActionOptions
        {
            obj.StatusToBeSet = status;
            return obj;
        }
    }
}
