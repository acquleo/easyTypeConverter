using easyTypeConverter.Common;
using easyTypeConverter.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Action.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    public abstract class DataEvaluatorActionOptions : ExtensibleOptions
    {

    }
}
