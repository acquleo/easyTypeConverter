using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PolymorphicAttribute : Attribute
    {
        public string TypeDiscriminatorPropertyName { get; set; }

        public PolymorphicAttribute(string typeDiscriminatorPropertyName = "$type")
        {
            TypeDiscriminatorPropertyName = typeDiscriminatorPropertyName;
        }
    }
}
