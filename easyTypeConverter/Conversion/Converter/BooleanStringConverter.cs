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
    public class BooleanStringConverter : TypeConverter
    {
        readonly BooleanStringConverterOptions options;
        public BooleanStringConverter(BooleanStringConverterOptions options) : base(options) {
            this.options = options;
        }
        public BooleanStringConverter():this(new BooleanStringConverterOptions()) { }

        public override List<DataType> SourceTypeList { get; } = new List<DataType>() { DataTypes.Boolean };
        public override List<DataType> TargetTypeList { get; } = new List<DataType>() {DataTypes.String };

        public override bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);

            outData = ((bool)inData).ToString(CultureInfoHelper.GetCultureInfo(this.options.Culture));

            if(this.options.Case == TextCase.Lower)
                outData = ((string)inData).ToLower(culture);
            if (this.options.Case == TextCase.Upper)
                outData = ((string)inData).ToLower(culture);

            return true;
        }

    }
}
