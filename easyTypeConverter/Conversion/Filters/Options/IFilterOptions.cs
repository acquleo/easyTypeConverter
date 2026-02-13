using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Filters.Options
{
    [Polymorphic(TypeDiscriminatorPropertyName = "$type")]
    [PolymorphicDerivedType(typeof(StringNumericBooleanFilterOptions), "str_bool")]
    [PolymorphicDerivedType(typeof(StringRegexMatchReplaceFilterOptions), "str_regex")]
    [PolymorphicDerivedType(typeof(StringRemovePrefixFilterOptions), "str_remove_prefix")]
    [PolymorphicDerivedType(typeof(StringReplaceFilterOptions), "str_replace")]
    [PolymorphicDerivedType(typeof(StringTrimFilterOptions), "str_trim")]
    public interface IFilterOptions: IExtensibleOptions
    {
        FilterAction Action { get; set; }
        Filter Build();
    }
}
