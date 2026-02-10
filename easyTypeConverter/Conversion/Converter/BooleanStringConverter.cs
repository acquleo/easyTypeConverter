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
    public class BooleanStringConverter : TypeConverter
    {
        readonly BooleanStringConverterOptions options;
        public BooleanStringConverter(BooleanStringConverterOptions options) : base(options) {
            this.options = options;
        }
        public BooleanStringConverter():this(new BooleanStringConverterOptions()) { }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(bool) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(string) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = ((bool)inData).ToString(this.options.Culture);

            if(this.options.Case == TextCase.Lower)
                outData = ((string)inData).ToLower(this.options.Culture);
            if (this.options.Case == TextCase.Upper)
                outData = ((string)inData).ToLower(this.options.Culture);

            return true;
        }

    }
}
