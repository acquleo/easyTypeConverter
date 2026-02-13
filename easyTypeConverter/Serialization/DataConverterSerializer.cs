using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Conversion.Filters.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class DataConverterSerializer
    {
        PolymorphicConverter<ITypeConverterOptions> extensibleConverter = new();
        PolymorphicConverter<IFilterOptions> filterConverter = new();

        public void RegisterTypeConverter<TDerived>(string typeDiscriminator) where TDerived : ITypeConverterOptions, new()
        {
            extensibleConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }

        public void RegisterFilter<TDerived>(string typeDiscriminator) where TDerived : IFilterOptions, new()
        {
            filterConverter.RegisterSubtype<TDerived>(typeDiscriminator);
        }
        public string Serialize(TypeConverterHandlerOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Serialize<TypeConverterHandlerOptions>(options, serializerOptions);
        }

        public TypeConverterHandlerOptions Deserialize(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Deserialize<TypeConverterHandlerOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }

        public string SerializeConverter(ITypeConverterOptions options)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Serialize<ITypeConverterOptions>(options, serializerOptions);
        }

        public ITypeConverterOptions DeserializeTransform(string json)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(extensibleConverter);
            serializerOptions.Converters.Add(filterConverter);
            return System.Text.Json.JsonSerializer.Deserialize<ITypeConverterOptions>(json, serializerOptions) ?? throw new InvalidOperationException("Deserialization failed");
        }
    }
}
