using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter
{
    public class StringDecimalConverter : TypeConverter
    {
        StringDecimalConverterOptions options;
        public StringDecimalConverter(StringDecimalConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringDecimalConverter(): this(new StringDecimalConverterOptions())
        {
            
        }

        public override List<DataType> SourceTypeList { get => new List<DataType>() { DataTypes.String }; }
        public override List<DataType> TargetTypeList { get => new List<DataType>() { DataTypes.Decimal }; }


        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);
            outData = default;

            if (!decimal.TryParse((string)inData, options.NumberStyle,
                culture, out var decimalParsed))
                return false;


            outData = decimalParsed;
            return true;
        }



    }
}
