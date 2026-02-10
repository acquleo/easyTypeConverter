using easyTypeConverter.Formatting.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter.Options
{    
    public class TemplateFormatterOptions : IValueFormatterOptions
    {
        public string Template {  get; set; } = string.Empty;
        public DateTimeKind TargetDateTimeKind { get; set; } = DateTimeKind.Unspecified;

        public ValueFormatter Build()
        {
            return new TemplateFormatter(this);
        }
    }
}
