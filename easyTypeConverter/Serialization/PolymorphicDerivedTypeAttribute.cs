using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PolymorphicDerivedTypeAttribute : Attribute
    {
        public Type DerivedType { get; }
        public string Discriminator { get; }

        public PolymorphicDerivedTypeAttribute(Type derivedType, string discriminator)
        {
            DerivedType = derivedType;
            Discriminator = discriminator;
        }
    }
}
