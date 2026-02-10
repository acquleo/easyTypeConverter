using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(AutoScaleDataUnitTransformerOptions), "autoscale_dataunit")]
    [JsonDerivedType(typeof(BitMaskTransformerOptions), "bitmask")]
    [JsonDerivedType(typeof(DataUnitTransformerOptions), "dataunit")]
    [JsonDerivedType(typeof(DeadbandTransformerOptions), "deadband")]
    [JsonDerivedType(typeof(PercentageNormalizerTransformerOptions), "percentage_norm")]
    [JsonDerivedType(typeof(ScalingTransformerOptions), "scaling")]
    public abstract class DataTransformerOptions
    {
        public abstract DataTransformer Build();
    }
}
