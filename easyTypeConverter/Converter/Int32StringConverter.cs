using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter
{
    public class Int32StringConverter : TypeConverter
    {
        readonly Int32StringConverterOptions options;
        public Int32StringConverter(Int32StringConverterOptions options) : base(options) {
            this.options = options;
        
        }
        public Int32StringConverter():this(new Int32StringConverterOptions()) { }
        public override Type SourceType { get => typeof(Int32); }
        public override Type TargetType { get => typeof(string); }

        public override bool OnConvert(object inData, [NotNullWhen(true)] out object? outData)
        {
            outData = ((Int32)inData).ToString(System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }


    }
}
