using easyTypeConverter.Common;
using easyTypeConverter.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluators.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(NcalcExpressionEvaluatorOptions), "exp")]

    public abstract class EvaluatorOptions : IExtensibleOptions
    {
        public abstract Evaluator Build(IEvaluatorContext context);
    }
}
