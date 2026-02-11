using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Common
{
    // Unità è una classe, non un enum
    public class DataType
    {
        public string Name { get; init; } = string.Empty;
        public Type Type { get; init; } = typeof(object);
        public override bool Equals(object? obj) =>
            obj is DataType other &&
            Type == other.Type;

        public override int GetHashCode() =>
            HashCode.Combine(Type.GetHashCode());

        public override string ToString() => Type.Name;
    }
}
