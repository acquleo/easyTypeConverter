
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
    /// <summary>
    /// Formatter that uses regex-based template processing to format values with placeholders enclosed in braces.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The formatter supports placeholders in the format: <c>{name}</c>, <c>{name:format}</c>, or <c>{t:name:format}</c>.
    /// </para>
    /// <para>
    /// Supported placeholders:
    /// <list type="bullet">
    /// <item><description><c>{value}</c> - The value to be formatted</description></item>
    /// <item><description><c>{unit}</c> - The symbol of the value's unit</description></item>
    /// <item><description><c>{status}</c> - The status information</description></item>
    /// <item><description><c>{timestamp}</c> - The timestamp value</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Format specifiers can be applied after a colon (e.g., <c>{timestamp:yyyy-MM-dd}</c>).
    /// The <c>t:</c> prefix marks a placeholder as translatable.
    /// </para>
    /// <para>
    /// To include literal braces in the output, use <c>&amp;ob;</c> for '{' and <c>&amp;cb;</c> for '}'.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var options = new TemplateFormatterOptions 
    /// { 
    ///     Template = "Value: {value:F2} {unit} at {timestamp:HH:mm:ss}",
    ///     TargetDateTimeKind = DateTimeKind.Utc
    /// };
    /// var formatter = new TemplateFormatter(options);
    /// </code>
    /// </example>
    public class TemplateFormatter : ValueFormatter
    {
        readonly TemplateFormatterOptions options;
        private static readonly Regex PlaceholderRegex = new(
            @"\{(?<translatable>t:)?(?<name>[a-zA-Z]+)(?::(?<format>[^}]+))?\}",
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
                var translatable = match.Groups["translatable"].Success;
                var name = match.Groups["name"].Value.ToLowerInvariant();
                var format = match.Groups["format"].Success
                    ? match.Groups["format"].Value
                    : null;

                var data =  name switch
                {
                    "value" => Format(inData.Value, format) ?? match.Value,
                    "unit" => Format(inData.ValueUnit?.Symbol, format) ?? match.Value,
                    "status" => Format(inData.Status, format) ?? match.Value,
                    "timestamp" => Format(inData.Timestamp, format) ?? match.Value,
                    _ => match.Value // unknown placeholder → leave as-is
                };
                return translatable ? data : data;
            });
            outData=outData
                .Replace("&ob;", "{")
                .Replace("&cb;", "}");
            return true;
        }

        /// <summary>
        /// FOrmat the value using the specified format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
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
