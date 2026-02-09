using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter.Options
{
    
    public static class IStringTextInputOptionsExtensions
    {
        public static T WithComparison<T>(this T obj, StringComparison comparison)
                where T : IStringTextInputOptions
        {
            obj.Comparison = comparison;
            return obj;
        }
    }
    

    public static class ITypeConverterOptionsExtensions
    {
        public static T AddInputFilter<T>(this T obj, IFilterOptions options)
                where T : ITypeConverterOptions
        {
            obj.InputFilters.Add(options);
            return obj;
        }

        public static T AddOutputFilter<T>(this T obj, IFilterOptions options)
                where T : ITypeConverterOptions
        {
            obj.OutputFilters.Add(options);
            return obj;
        }
    }

    public static class IStringNumericConverterBaseOptionsExtensions
    {
        public static T WithNumberStyle<T>(this T obj, NumberStyles style)
                where T : IStringNumericConverterBaseOptions
        {
            obj.NumberStyle = style;
            return obj;
        }
        public static T WithCulture<T>(this T obj, CultureInfo culture)
               where T : IStringNumericConverterBaseOptions
        {
            obj.Culture = culture;
            return obj;
        }
        public static T WithHexDetection<T>(this T obj)
                where T : IStringNumericConverterBaseOptions
        {
            obj.HexDetection = true;
            return obj;
        }
        public static T WithAddPrefix<T>(this T obj, string prefix)
                where T : IStringNumericConverterBaseOptions
        {
            if(!obj.HexPrefixList.Contains(prefix))
                obj.HexPrefixList.Add(prefix);
            return obj;
        }
    }
}
