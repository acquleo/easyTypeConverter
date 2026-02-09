using easyTypeConverter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Filters.Options
{
    /// <summary>
    /// Rimuove un prefisso dalla stringa se presente
    /// </summary>
    public class StringRemovePrefixFilterOptions : IStringRemovePrefixFilterOptions
    {
        public string Prefix { get; set; } = string.Empty;
        public StringComparison Comparison { get; set; } =  StringComparison.InvariantCulture;
        public FilterAction Action { get; set; } = FilterAction.Continue;

        public Filter Build()
        {
            return new StringPrefixRemoverFilter(this);
        }
    }
}
