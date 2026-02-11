using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public static class DataTransformSerializer
    {

        public static string Serialize(DataTransformerHandlerOptions options)
        {
            return System.Text.Json.JsonSerializer.Serialize< DataTransformerHandlerOptions>(options);
        }

        public static DataTransformerHandlerOptions Deserialize(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<DataTransformerHandlerOptions>(json) ?? throw new InvalidOperationException("Deserialization failed");
        }

        public static string SerializeTransformer(DataTransformerOptions options)
        {
            return System.Text.Json.JsonSerializer.Serialize<DataTransformerOptions>(options);
        }

        public static DataTransformerOptions DeserializeTransform(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<DataTransformerOptions>(json) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
