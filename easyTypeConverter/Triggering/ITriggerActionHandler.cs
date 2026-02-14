using easyTypeConverter.Triggering.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering
{
    public interface ITriggerActionHandler
    {
        public bool Handle(TriggerInputContext? inputContext, TriggerActionOptions options);
    }
}
