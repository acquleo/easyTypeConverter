
using easyTypeConverter.Common;
using easyTypeConverter.Formatting.Formatter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter
{
    public class TemplateFormatter : ValueFormatter
    {
        readonly TemplateFormatterOptions options;
        private static readonly Regex PlaceholderRegex = new(
           @"\{(?<name>[a-zA-Z]+)(?::(?<format>[^}]+))?\}",
           RegexOptions.Compiled);
        readonly DateTimeConverter dateTimeConverter = new DateTimeConverter();
        public TemplateFormatter(TemplateFormatterOptions options)
        {
            this.options = options;
        }

        protected override bool OnFormat(FormatterContext inData, [NotNullWhen(true)] out string? outData)
        {
            outData = PlaceholderRegex.Replace(options.Template, match =>
            {
                var name = match.Groups["name"].Value.ToLowerInvariant();
                var format = match.Groups["format"].Success
                    ? match.Groups["format"].Value
                    : null;

                return name switch
                {
                    "value" => Format(inData.Value, format) ?? match.Value,
                    "unit" => Format(inData.ValueUnit?.Symbol, format) ?? match.Value,
                    "status" => Format(inData.Status, format) ?? match.Value,
                    "timestamp" => Format(inData.Timestamp, format) ?? match.Value,
                    _ => match.Value // unknown placeholder → leave as-is
                };
            });
            return true;
        }

        private string? Format(object? value, string? format)
        {
            if (value is null)
                return null;

            if (format is null)
                return value.ToString();

            if (value is DateTime dt)
            {
                value = dateTimeConverter.Convert(dt, options.TargetDateTimeKind);
            }

            if (value is DateTimeOffset dto)
            {
                value = dateTimeConverter.Convert(dto, options.TargetDateTimeKind);
            }

            // IFormattable covers DateTime, TimeSpan, numeric types, enums...
            if (value is IFormattable formattable)
                return formattable.ToString(format, null);

            return value.ToString();
        }
    }
}
