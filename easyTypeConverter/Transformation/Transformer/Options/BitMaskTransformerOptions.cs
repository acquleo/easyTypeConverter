using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public class BitMaskTransformerOptions : DataTransformerOptions
    {
        [JsonPropertyName("mask")]
        public ulong Mask { get; set; }
        public bool Normalize { get; set; }
        public override DataTransformer Build()
        {
            return new BitMaskTransformer(this);
        }
    }
}
