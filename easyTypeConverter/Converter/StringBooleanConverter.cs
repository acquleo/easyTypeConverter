using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter
{
    /// <summary>
    /// Converte una string True False in boolean
    /// </summary>
    public class StringBooleanConverter : TypeConverter
    {
        readonly StringBooleanConverterOptions options;
        public StringBooleanConverter(StringBooleanConverterOptions options):base(options) 
        {
            this.options = options;
        }

        public StringBooleanConverter():this(new StringBooleanConverterOptions()) { }

        public override Type SourceType { get => typeof(string); }
        public override Type TargetType { get => typeof(bool); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = default;

            if (!bool.TryParse((string)inData, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
