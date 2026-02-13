using easyTypeConverter.Evaluating.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating
{
    public interface IDataEvaluatorActionHandler
    {
        public bool Handle(DataEvaluatorInputContext? inputContext, DataEvaluatorActionOptions options);
    }
}
