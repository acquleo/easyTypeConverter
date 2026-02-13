using easyTypeConverter.Common;
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
    /// Converte una stringa in un valore booleano.
    /// </summary>
    public class StringBooleanConverter : TypeConverter
    {
        readonly StringBooleanConverterOptions options;
        public StringBooleanConverter(StringBooleanConverterOptions options):base(options) 
        {
            this.options = options;
        }

        public StringBooleanConverter():this(new StringBooleanConverterOptions()) { }

        public override List<DataType> SourceTypeList { get => new List<DataType>() { DataTypes.String }; }
        public override List<DataType> TargetTypeList { get => new List<DataType>() { DataTypes.Boolean }; }

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = default;

            if (inData is not string inputString)
                return false;

            var processedString = inputString.Trim();

            if (options.TrueValues.Exists(value => string.Equals(value, processedString, StringComparison.OrdinalIgnoreCase)))
            {
                outData = true;
                return true;
            }

            if (options.FalseValues.Exists(value => string.Equals(value, processedString, StringComparison.OrdinalIgnoreCase)))
            {
                outData = false;
                return true;
            }

            return false;
        }

    }
}
