using easyTypeConverter.Filters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Filters
{
    /// <summary>
    /// rimuove un prefisso da una stringa se presente
    /// </summary>
    public class StringNumericBooleanFilter : Filter
    {
        StringNumericBooleanFilterOptions options;
        public StringNumericBooleanFilter(StringNumericBooleanFilterOptions options)
        {
            this.options = options;
            
        }
        public override Type Type => typeof(string);

        public override FilterAction OnProcess(object inData, out object outData)
        {
            outData = inData;
            if (long.TryParse((string)inData, options.NumberStyle, options.Culture, out long number))
            {
                outData = (number != 0).ToString();
                return options.Action;
            }

            return FilterAction.Continue;
        }
        
    }
}
