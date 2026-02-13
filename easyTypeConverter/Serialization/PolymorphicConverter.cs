using easyTypeConverter.Common;
using easyTypeConverter.Evaluating.Action.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    public class PolymorphicConverter<TBase> : JsonConverter<TBase> where TBase : IExtensibleOptions
    {
        private readonly Dictionary<string, Type> _userTypes = new();
        private readonly string _discriminator;

        public PolymorphicConverter()
        {
            var rootType = typeof(TBase);

            // Get discriminator property name from [JsonPolymorphic]
            var polymorphicAttr = rootType.GetCustomAttribute<PolymorphicAttribute>();
            _discriminator = polymorphicAttr?.TypeDiscriminatorPropertyName ?? "$type";

            // Auto-register all [JsonDerivedType] attributes
            var derivedTypeAttrs = rootType.GetCustomAttributes<PolymorphicDerivedTypeAttribute>();
            foreach (var attr in derivedTypeAttrs)
            {
                if (attr.Discriminator is string key && !string.IsNullOrEmpty(key))
                    _userTypes.Add(key, attr.DerivedType);
            }
        }

        public void RegisterSubtype<TDerived>(string typeDiscriminator) where TDerived : TBase
        {
            _userTypes[typeDiscriminator] = typeof(TDerived);
        }

        public override TBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var jsonObject = jsonDoc.RootElement;

            if (!jsonObject.TryGetProperty(_discriminator, out var typeProp))
                throw new JsonException($"Missing Type discriminator '{_discriminator}'");

            var typeName = typeProp.GetString();

            // Check user-registered types first
            if (_userTypes.TryGetValue(typeName!, out var userType))
            {
                return (TBase)JsonSerializer.Deserialize(jsonObject.GetRawText(), userType, options)!;
            }

            // Otherwise, fallback to default polymorphic deserialization (built-in types via attributes)
            return (TBase)JsonSerializer.Deserialize(jsonObject.GetRawText(), typeToConvert, options)!;
        }

        public override void Write(Utf8JsonWriter writer, TBase value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            var typeName = _userTypes.FirstOrDefault(x => x.Value == type).Key;

            if (typeName != null)
            {
                // It's a user type
                using var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(value, type, options));
                writer.WriteStartObject();
                writer.WriteString(_discriminator, typeName);
                foreach (var prop in jsonDoc.RootElement.EnumerateObject())
                    prop.WriteTo(writer);
                writer.WriteEndObject();
            }
            else
            {
                // Built-in types handled by default polymorphic serializer
                JsonSerializer.Serialize(writer, value, type, options);
            }
        }
    }
}
