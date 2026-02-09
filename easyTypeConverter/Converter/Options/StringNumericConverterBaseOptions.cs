using easyTypeConverter.Filters;
using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    public abstract class StringNumericConverterBaseOptions :
        IStringNumericConverterBaseOptions
    {
        public NumberStyles NumberStyle { get; set; } = NumberStyles.Any;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public bool HexDetection { get; set; } = false;
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public List<string> HexPrefixList { get; set; } = new() { "0x", "0X", "&h" };

        public abstract TypeConverter OnBuild();

        public TypeConverter Build()
        {
            return OnBuild();
        }
    }
}
