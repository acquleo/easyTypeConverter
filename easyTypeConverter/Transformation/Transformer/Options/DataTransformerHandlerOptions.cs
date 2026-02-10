using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public class DataTransformerHandlerOptions
    {
        [JsonPropertyName("transformers")]
        public List<DataTransformerOptions> Transformers { get; set; } = new List<DataTransformerOptions>();

        public DataTransformerHandler Build()
        {
            var handler = new DataTransformerHandler();
            foreach (var transformerOptions in Transformers)
            {
                handler.AddTransformer(transformerOptions);
            }
            return handler;
        }
    }
}
