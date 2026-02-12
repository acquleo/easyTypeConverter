using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class StringBooleanConverterOptions : IStringBooleanConverterOptions
    {
        public bool DefaultOutput { get; set; } = false;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();

        /// <summary>
        /// Elenco di stringhe che verranno convertite in <c>true</c>. I valori predefiniti sono "true", "1", "yes", "on", "active", "enabled", "enable". Il confronto non fa distinzione tra maiuscole e minuscole.
        /// </summary>
        public List<string> TrueValues { get; set; } = new List<string> { "true", "1", "yes", "on", "active", "enabled", "enable" };

        /// <summary>
        /// Elenco di stringhe che verranno convertite in <c>false</c>. I valori predefiniti sono "false", "0", "no", "off", "inactive", "disabled", "disable". Il confronto non fa distinzione tra maiuscole e minuscole.
        /// </summary>
        public List<string> FalseValues { get; set; } = new List<string> { "false", "0", "no", "off", "inactive", "disabled", "disable" };

        public TypeConverter Build()
        {
            return new StringBooleanConverter(this);
        }
    }
}
