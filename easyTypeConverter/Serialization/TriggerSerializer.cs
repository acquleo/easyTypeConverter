using easyTypeConverter.Transformation.Transformer.Options;
using easyTypeConverter.Triggering.Action.Options;
using easyTypeConverter.Triggering.Evaluator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class TriggerSerializer
    {
        PolymorphicConverter<TriggerActionOptions> extensibleConverter = new();
        PolymorphicConverter<TriggerOptions> evaluatorConverter = new();
        public TriggerSerializer()
        {

        }

        public void RegisterEvaluator<TDerived>(string typeDiscriminator) where TDerived : TriggerOptions, new()
        {
            evaluatorConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public void RegisterEvaluatorAction<TDerived>(string typeDiscriminator) where TDerived : TriggerActionOptions,new()
        {
            extensibleConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public string SerializeEvaluator(TriggerOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(evaluatorConverter);
            return System.Text.Json.JsonSerializer.Serialize<TriggerOptions>(options, serializerOptions);
        }

        public TriggerOptions DeserializeEvaluator(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(evaluatorConverter);

            return System.Text.Json.JsonSerializer.Deserialize<TriggerOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
