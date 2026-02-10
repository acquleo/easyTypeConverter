using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Filters
{
    /// <summary>
    /// rimuove un prefisso da una stringa se presente
    /// </summary>
    public class StringTrimFilter : Filter
    {
        IStringTrimFilterOptions options;
        public StringTrimFilter(IStringTrimFilterOptions options)
        {
            this.options = options;

            
        }
        public override Type Type => typeof(string);

        public override FilterAction OnProcess(object inData, out object outData)
        {
            outData = ((string)inData).Trim();
            return FilterAction.Continue;
        }
    }
}
