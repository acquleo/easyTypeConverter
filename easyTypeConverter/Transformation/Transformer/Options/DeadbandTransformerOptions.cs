using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class DeadbandTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("deadband")]
        public double Deadband { get; set; } = 0;

        public override DataTransformer Build()
        {
            return new DeadbandTransformer(this);
        }
    }
}
