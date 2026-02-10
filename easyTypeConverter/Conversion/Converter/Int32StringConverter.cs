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
    public class Int32StringConverter : TypeConverter
    {
        readonly Int32StringConverterOptions options;
        public Int32StringConverter(Int32StringConverterOptions options) : base(options) {
            this.options = options;
        
        }
        public Int32StringConverter():this(new Int32StringConverterOptions()) { }
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(Int32) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(string) }; }

        public override bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData)
        {
            outData = ((Int32)inData).ToString(System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }


    }
}
