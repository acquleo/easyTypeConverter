using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter
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

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(string) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(bool) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = default;

            if (!bool.TryParse((string)inData, out var toutData))
                return false;

            outData = toutData;
            return true;
        }

    }
}
