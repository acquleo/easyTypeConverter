using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace easyTypeConverter
{
    public abstract class TypeConverterContextBase
    {
        public void Add(TypeConverter typeConverter) { 
            typeConverters.Add(typeConverter);
        }
        readonly List<TypeConverter> typeConverters = new();
        public IEnumerable<TypeConverter> GetConverters()
        {
            return typeConverters;
        }
    }
}
