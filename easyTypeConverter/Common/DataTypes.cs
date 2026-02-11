using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace easyTypeConverter.Common
{
    public static class DataTypes
    {
        public static readonly DataType Boolean = new() { Name = "Boolean", Type = typeof(Boolean) };
        public static readonly DataType Byte = new() { Name = "Byte", Type = typeof(Byte) };
        public static readonly DataType SByte = new() { Name = "SByte", Type = typeof(SByte) };
        public static readonly DataType Char = new() { Name = "Char", Type = typeof(Char) };
        public static readonly DataType Decimal = new() { Name = "Decimal", Type = typeof(Decimal) };
        public static readonly DataType Double = new() { Name = "Double", Type = typeof(Double) };
        public static readonly DataType Single = new() { Name = "Single", Type = typeof(Single) };
        public static readonly DataType Int16 = new() { Name = "Int16", Type = typeof(Int16) };
        public static readonly DataType UInt16 = new() { Name = "UInt16", Type = typeof(UInt16) };
        public static readonly DataType Int32 = new() { Name = "Int32", Type = typeof(Int32) };
        public static readonly DataType UInt32 = new() { Name = "UInt32", Type = typeof(UInt32) };
        public static readonly DataType Int64 = new() { Name = "Int64", Type = typeof(Int64) };
        public static readonly DataType UInt64 = new() { Name = "UInt64", Type = typeof(UInt64) };
        public static readonly DataType String = new() { Name = "String", Type = typeof(String) };
        public static readonly DataType TimeSpan = new() { Name = "TimeSpan", Type = typeof(TimeSpan) };
        public static readonly DataType DateTime = new() { Name = "DateTime", Type = typeof(DateTime) };


        // ── LOOKUP ────────────────────────────────────────
        public static bool FromName(string name, [NotNullWhen(true)] out DataType? unit)
        {
            var f_unit = All
                .Where(u => u.Name == name)
                .FirstOrDefault();

            if(f_unit != null)
            {
                unit = f_unit;
                return true;
            }

            unit = null;
            return false;

        }

        public static DataType FromName(string name)
        {
            var f_unit = All
                .Where(u => u.Name == name)
                .FirstOrDefault();

            if (f_unit != null)
            {
                return f_unit;
            }

            throw new InvalidOperationException($"DataType with name '{name}' not found.");

        }
        public static DataType FromType(Type type)
        {
            var f_unit = All
                .Where(u => u.Type == type)
                .FirstOrDefault();

            if (f_unit != null)
            {
                return f_unit;
            }

            throw new InvalidOperationException($"DataType with typename '{type}' not found.");

        }
        public static bool FromType(Type type, [NotNullWhen(true)] out DataType? unit)
        {
            var f_unit = All
                .Where(u => u.Type == type)
                .FirstOrDefault();

            if (f_unit != null)
            {
                unit = f_unit;
                return true;
            }

            unit = null;
            return false;

        }

        // Tutti i valori per lookup/validazione
        public static IEnumerable<DataType> All => 
            typeof(DataTypes)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.FieldType == typeof(DataType))
            .Select(f => f.GetValue(null) as DataType)
            .Where(f => f != null);
    }
}
