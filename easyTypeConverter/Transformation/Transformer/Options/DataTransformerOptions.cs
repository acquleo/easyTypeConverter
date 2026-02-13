using easyTypeConverter.Common;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(AutoScaleDataUnitTransformerOptions), "autoscale_dataunit")]
    [PolymorphicDerivedType(typeof(BitMaskTransformerOptions), "bitmask")]
    [PolymorphicDerivedType(typeof(DataUnitTransformerOptions), "dataunit")]
    [PolymorphicDerivedType(typeof(DeadbandTransformerOptions), "deadband")]
    [PolymorphicDerivedType(typeof(PercentageNormalizerTransformerOptions), "percentage_norm")]
    [PolymorphicDerivedType(typeof(ScalingTransformerOptions), "scaling")]
    [PolymorphicDerivedType(typeof(TypeConverterTransformerOptions), "type_converter")]
    public abstract class DataTransformerOptions : IExtensibleOptions
    {
        public abstract DataTransformer Build();
    }
}
