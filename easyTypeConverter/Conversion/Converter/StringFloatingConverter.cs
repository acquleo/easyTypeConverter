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
    public class StringFloatingConverter : TypeConverter
    {
        StringFloatingConverterOptions options;
        public StringFloatingConverter(StringFloatingConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringFloatingConverter(): this(new StringFloatingConverterOptions())
        {
            
        }

        public override List<DataType> SourceTypeList { get => new List<DataType>() { DataTypes.String }; }
        public override List<DataType> TargetTypeList { get => new List<DataType>() { DataTypes.Single, DataTypes.Double }; }


        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);
            outData = default;

            if (!double.TryParse((string)inData, options.NumberStyle,
                culture, out var doubleParsed))
                return false;


            outData = System.Convert.ChangeType(doubleParsed, targetType.Type); ;
            return true;
        }



    }
}
