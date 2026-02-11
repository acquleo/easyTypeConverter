using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter.Options
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(BooleanStringConverterOptions), "bool_str")]
    [JsonDerivedType(typeof(FloatingConverterOptions), "float")]
    [JsonDerivedType(typeof(IntegralConverterOptions), "num")]
    [JsonDerivedType(typeof(NumberTimeSpanConverterOptions), "num_timespan")]
    [JsonDerivedType(typeof(StringBooleanConverterOptions), "str_bool")]
    [JsonDerivedType(typeof(StringDecimalConverterOptions), "str_dec")]
    [JsonDerivedType(typeof(StringFloatingConverterOptions), "str_float")]
    [JsonDerivedType(typeof(StringNumericConverterOptions), "str_num")]
    [JsonDerivedType(typeof(StringTimeSpanConverterOptions), "str_timespan")]
    [JsonDerivedType(typeof(TimeSpanNumberConverterOptions), "timespan_num")]
    [JsonDerivedType(typeof(TimeSpanStringConverterOptions), "timespan_str")]
    public interface ITypeConverterOptions
    {
        List<IFilterOptions> InputFilters { get; }
        List<IFilterOptions> OutputFilters { get; }
        TypeConverter Build();
    }
}
