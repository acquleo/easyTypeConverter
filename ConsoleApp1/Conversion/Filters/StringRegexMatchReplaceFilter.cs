using ConsoleApp1.Conversion.Filters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Conversion.Filters
{
    /// <summary>
    /// rimuove un prefisso da una stringa se presente
    /// </summary>
    public class StringRegexMatchReplaceFilter : Filter
    {
        StringRegexMatchReplaceFilterOptions options;
        Dictionary<string, string> replaces;
        public StringRegexMatchReplaceFilter(StringRegexMatchReplaceFilterOptions options)
        {
            this.options = options;

            replaces=new Dictionary<string, string>();

            foreach (var match in options.Matches)
            {
                this.replaces.TryAdd(match.Item1, match.Item2);
            }

            
        }
        public override Type Type => typeof(string);

        public override FilterAction OnProcess(object inData, out object outData)
        {
            outData = inData;

            foreach (var element in replaces)
            {
                Regex regex = new Regex(element.Key);
                if (!regex.Match((string)inData).Success)
                {
                    continue;
                }

                outData = element.Value;
                return options.Action;
            }

            return  FilterAction.Continue;
        }

    }
}
