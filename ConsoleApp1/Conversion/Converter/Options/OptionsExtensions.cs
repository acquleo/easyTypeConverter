using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Converter.Options
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

    public static class StringByteConverterOptionsExtensions
    {
        public static T WithNumberStyle<T>(this T obj, NumberStyles style)
                where T : StringByteConverterOptions
        {
            obj.NumberStyle = style;
            return obj;
        }
        public static T WithCulture<T>(this T obj, CultureInfo culture)
               where T : StringByteConverterOptions
        {
            obj.Culture = culture;
            return obj;
        }
    }
}
