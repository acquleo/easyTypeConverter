using easyTypeConverter.Formatting.Formatters;
using easyTypeConverter.Transformation.Transformers.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatters.Options
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(TemplateFormatterOptions), "template")]
    public abstract class ValueFormatterOptions
    {
        public abstract ValueFormatter Build();
    }
}
