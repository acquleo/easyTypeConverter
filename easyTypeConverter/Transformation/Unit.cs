using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    // Unità è una classe, non un enum
    public class Unit
    {
        public string Symbol { get; init; } = string.Empty;        // "MB", "kg", "°C"
        public string Name { get; init; } = string.Empty;          // "Megabyte", "Kilogram"
        public UnitCategory Category { get; init; }
        public bool IsBinary { get; init; } = false;                // Solo per Data

        // Uguaglianza per symbol + categoria
        public override bool Equals(object? obj) =>
            obj is Unit other &&
            Symbol == other.Symbol &&
            Category == other.Category;

        public override int GetHashCode() =>
            HashCode.Combine(Symbol, Category);

        public override string ToString() => Symbol;
    }
}
