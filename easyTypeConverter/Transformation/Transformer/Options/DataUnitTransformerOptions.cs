using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class DataUnitTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("sourceUnit")]
        public DataUnit SourceUnit { get; set; } = DataUnit.Byte;
        [JsonPropertyName("targetUnit")]
        public DataUnit TargetUnit { get; set; } = DataUnit.Megabyte;

        public override DataTransformer Build()
        {
            return new DataUnitTransformer();
        }
    }
}
