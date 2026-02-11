using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public class TypeConverterTransformerOptions : DataTransformerOptions
    {

        [JsonPropertyName("converter")]
        public ITypeConverterOptions? Converter { get; set; }
        
        public override DataTransformer Build()
        {
            return new TypeConverterTransformer(this);
        }
    }
}
