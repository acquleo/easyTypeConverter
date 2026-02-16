using easyTypeConverter.Conversion.Converters.Options;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Transformation.Transformers.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class DataTransformSerializer
    {
        PolymorphicConverter<DataTransformerOptions> transformerConverter = new();
        PolymorphicConverter<ITypeConverterOptions> extensibleConverter = new();
        PolymorphicConverter<IFilterOptions> filterConverter = new();
        public void RegisterDataTransformer<TDerived>(string typeDiscriminator) where TDerived : DataTransformerOptions, new()
        {
            transformerConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public void RegisterTypeConverter<TDerived>(string typeDiscriminator) where TDerived : ITypeConverterOptions, new()
        {
            extensibleConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public void RegisterFilter<TDerived>(string typeDiscriminator) where TDerived : IFilterOptions, new()
        {
            filterConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public string Serialize(DataTransformerHandlerOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(transformerConverter);
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Serialize<DataTransformerHandlerOptions>(options, serializerOptions);
        }

        public DataTransformerHandlerOptions Deserialize(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(transformerConverter);
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Deserialize<DataTransformerHandlerOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }

        public string SerializeTransformer(DataTransformerOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(transformerConverter);
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Serialize<DataTransformerOptions>(options, serializerOptions);
        }

        public DataTransformerOptions DeserializeTransform(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(transformerConverter);
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Deserialize<DataTransformerOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
