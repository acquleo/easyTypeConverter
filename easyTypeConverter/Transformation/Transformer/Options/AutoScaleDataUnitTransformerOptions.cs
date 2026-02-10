using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{

    public class AutoScaleDataUnitTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("sourceUnit")]
        public DataUnit SourceUnit { get; set; } = DataUnit.Byte;
        [JsonPropertyName("useBinary")]
        public bool UseBinary { get; set; } = true;
        [JsonPropertyName("scaleThreshold")]
        public double ScaleThreshold { get; set; } = 1.0;

        public override DataTransformer Build()
        {
            return new AutoScaleDataUnitTransformer(this);
        }
    }
}
