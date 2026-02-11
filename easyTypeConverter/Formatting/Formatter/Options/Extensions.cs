using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter.Options
{
    public static class TemplateFormatterOptionsExtensions
    {
        public static T WithTemplate<T>(this T obj, string template)
               where T : TemplateFormatterOptions
        {
            obj.Template = template;
            return obj;
        }
        public static T WithDateTimeConvert<T>(this T obj, DateTimeKind convertTo)
              where T : TemplateFormatterOptions
        {
            obj.TargetKind = convertTo;
            return obj;
        }
    }

}
