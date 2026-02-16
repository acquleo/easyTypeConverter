using easyTypeConverter.Formatting.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatters.Options
{
    public class TemplateFormatterOptions : ValueFormatterOptions
    {
        [JsonPropertyName("template")]
        public string Template {  get; set; } = string.Empty;
        [JsonPropertyName("targetKind")]
        public DateTimeKind TargetKind { get; set; } = DateTimeKind.Unspecified;

        public override ValueFormatter Build()
        {
            return new TemplateFormatter(this);
        }
    }
}
