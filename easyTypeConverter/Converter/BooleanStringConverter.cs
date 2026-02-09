using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter
{
    public class BooleanStringConverter : TypeConverter
    {
        readonly BooleanStringConverterOptions options;
        public BooleanStringConverter(BooleanStringConverterOptions options) : base(options) {
            this.options = options;
        }
        public BooleanStringConverter():this(new BooleanStringConverterOptions()) { }

        public override Type SourceType { get => typeof(bool); }
        public override Type TargetType { get => typeof(string); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
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
