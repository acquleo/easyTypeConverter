using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class ScalingTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("inputMin")]
        public double InputMin { get; set; } = 0;
        [JsonPropertyName("inputMax")]
        public double InputMax { get; set; } = 1.0;
        [JsonPropertyName("outputMin")]
        public double OutputMin { get; set; } = 0;
        [JsonPropertyName("outputMax")]
        public double OutputMax { get; set; } = 1.0;

        public override DataTransformer Build()
        {
            return new ScalingTransformer(this);
        }
    }
}
