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
    public class StringReplaceFilter : Filter
    {
        StringReplaceFilterOptions options;
        Dictionary<string, string> replaces;
        public StringReplaceFilter(StringReplaceFilterOptions options)
        {
            this.options = options;

            replaces=new Dictionary<string, string>(StringComparer.FromComparison(options.Comparison));

            foreach (var match in options.Matches)
            {
                this.replaces.TryAdd(match.Item1, match.Item2);
            }

            
        }
        public override Type Type => typeof(string);

        public override FilterAction OnProcess(object inData, out object outData)
        {
            outData = inData;

            if (!replaces.ContainsKey((string)inData))
                return  FilterAction.Continue;


            outData = replaces[(string)inData];
            return options.Action;
        }

    }
}
