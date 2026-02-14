using easyTypeConverter.Evaluating.Evaluators.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class EvaluatorSerializer
    {
        PolymorphicConverter<EvaluatorOptions> extensibleConverter = new();
        public EvaluatorSerializer()
        {

        }

        public void RegisterEvaluator<TDerived>(string typeDiscriminator) where TDerived : EvaluatorOptions\, new()
        {
            extensibleConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public string SerializeEvaluator(EvaluatorOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            return System.Text.Json.JsonSerializer.Serialize<EvaluatorOptions>(options, serializerOptions);
        }

        public EvaluatorOptions DeserializeEvaluator(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
        
            return System.Text.Json.JsonSerializer.Deserialize<EvaluatorOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
