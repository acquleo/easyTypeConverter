using easyTypeConverter.Common;
using easyTypeConverter.Evaluating.Action.Options;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Evaluating.Evaluator.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(EqualityDataEvaluatorOptions), "eq")]
    [PolymorphicDerivedType(typeof(ExpressionEvaluatorOptions), "exp")]

    public abstract class DataEvaluatorOptions : ExtensibleOptions
    {
        public List<DataEvaluatorActionOptions> Actions { get; set; } = new List<DataEvaluatorActionOptions>();
        public abstract DataEvaluator Build(IDataEvaluatorActionHandler actionHandler);
    }
}
