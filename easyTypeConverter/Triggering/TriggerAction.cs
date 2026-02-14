using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Triggering
{
    public abstract class TriggerAction
    {
        public abstract void Handle(TriggerInputContext? inputContext);
    }
}
