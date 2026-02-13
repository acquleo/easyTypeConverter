using easyTypeConverter.Evaluating.Action.Options;
using easyTypeConverter.Evaluating.Evaluator.Options;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class DataEvaluatorSerializer
    {
        PolymorphicConverter<DataEvaluatorActionOptions> extensibleConverter = new();
        PolymorphicConverter<DataEvaluatorOptions> evaluatorConverter = new();
        public DataEvaluatorSerializer()
        {

        }

        public void RegisterEvaluator<TDerived>(string typeDiscriminator) where TDerived : DataEvaluatorOptions, new()
        {
            evaluatorConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public void RegisterEvaluatorAction<TDerived>(string typeDiscriminator) where TDerived : DataEvaluatorActionOptions,new()
        {
            extensibleConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public string SerializeEvaluator(DataEvaluatorOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(evaluatorConverter);
            return System.Text.Json.JsonSerializer.Serialize<DataEvaluatorOptions>(options, serializerOptions);
        }

        public DataEvaluatorOptions DeserializeEvaluator(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(evaluatorConverter);

            return System.Text.Json.JsonSerializer.Deserialize<DataEvaluatorOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
