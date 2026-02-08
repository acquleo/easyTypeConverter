using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters
{
    /// <summary>
    /// rimuove un prefisso da una stringa se presente
    /// </summary>
    public class StringPrefixRemoverFilter : Filter
    {
        StringRemovePrefixFilterOptions options;
        public StringPrefixRemoverFilter(StringRemovePrefixFilterOptions options)
        {
            this.options = options;
        }
        public override Type Type => typeof(string);

        public override FilterAction OnProcess(object inData, out object outData)
        {
            outData = inData;
            if (string.IsNullOrEmpty(options.Prefix)) return  FilterAction.Continue;

            if (((string)inData).StartsWith(options.Prefix, StringComparison.InvariantCulture))
            {
                outData = ((string)inData).Substring(options.Prefix.Length);
            }
            return FilterAction.Continue;
        }

    }
}
