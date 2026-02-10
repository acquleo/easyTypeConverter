using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class PercentageNormalizerTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("min")]
        public double Min { get; set; } = 0;
        [JsonPropertyName("max")]
        public double Max { get; set; } = 1.0;
        public override DataTransformer Build()
        {
            return new PercentageNormalizerTransformer();
        }
    }
}
