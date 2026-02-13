using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(BooleanStringConverterOptions), "bool_str")]
    [PolymorphicDerivedType(typeof(FloatingConverterOptions), "float")]
    [PolymorphicDerivedType(typeof(FloatingIntegralConverterOptions), "float_int")]
    [PolymorphicDerivedType(typeof(IntegralConverterOptions), "int")]
    [PolymorphicDerivedType(typeof(NumberTimeSpanConverterOptions), "num_ts")]
    [PolymorphicDerivedType(typeof(StringBooleanConverterOptions), "str_bool")]
    [PolymorphicDerivedType(typeof(StringDecimalConverterOptions), "str_dec")]
    [PolymorphicDerivedType(typeof(StringFloatingConverterOptions), "str_float")]
    [PolymorphicDerivedType(typeof(StringNumericConverterOptions), "str_num")]
    [PolymorphicDerivedType(typeof(StringTimeSpanConverterOptions), "str_ts")]
    [PolymorphicDerivedType(typeof(TimeSpanNumberConverterOptions), "ts_num")]
    [PolymorphicDerivedType(typeof(TimeSpanStringConverterOptions), "ts_str")]
    public interface ITypeConverterOptions : IExtensibleOptions
    {
        List<IFilterOptions> InputFilters { get; set;  }
        List<IFilterOptions> OutputFilters { get; set; }
        TypeConverter Build();
    }
}
