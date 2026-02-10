using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public static class Units
    {
        // ── DATA ──────────────────────────────────────────
        public static class Data
        {
            public static readonly Unit Bit = new() { Symbol = "bit", Name = "Bit", Category = UnitCategory.Data };
            public static readonly Unit Byte = new() { Symbol = "B", Name = "Byte", Category = UnitCategory.Data };

            public static readonly Unit Kibibyte = new() { Symbol = "KiB", Name = "Kibibyte", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Mebibyte = new() { Symbol = "MiB", Name = "Mebibyte", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Gibibyte = new() { Symbol = "GiB", Name = "Gibibyte", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Tebibyte = new() { Symbol = "TiB", Name = "Tebibyte", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Pebibyte = new() { Symbol = "PiB", Name = "Pebibyte", Category = UnitCategory.Data, IsBinary = true };

            public static readonly Unit Kilobyte = new() { Symbol = "KB", Name = "Kilobyte", Category = UnitCategory.Data };
            public static readonly Unit Megabyte = new() { Symbol = "MB", Name = "Megabyte", Category = UnitCategory.Data };
            public static readonly Unit Gigabyte = new() { Symbol = "GB", Name = "Gigabyte", Category = UnitCategory.Data };
            public static readonly Unit Terabyte = new() { Symbol = "TB", Name = "Terabyte", Category = UnitCategory.Data };
            public static readonly Unit Petabyte = new() { Symbol = "PB", Name = "Petabyte", Category = UnitCategory.Data };

            public static readonly Unit Kibibit = new() { Symbol = "Kibit", Name = "Kibibit", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Mebibit = new() { Symbol = "Mebibit", Name = "Mebibit", Category = UnitCategory.Data, IsBinary = true };
            public static readonly Unit Gibibit = new() { Symbol = "Gibibit", Name = "Gibibit", Category = UnitCategory.Data, IsBinary = true };

            public static readonly Unit Kilobit = new() { Symbol = "Kb", Name = "Kilobit", Category = UnitCategory.Data };            
            public static readonly Unit Megabit = new() { Symbol = "Mbit", Name = "Megabit", Category = UnitCategory.Data };
            public static readonly Unit Gigabit = new() { Symbol = "Gbit", Name = "Gigabit", Category = UnitCategory.Data };
        }

        // ── WEIGHT ────────────────────────────────────────
        public static class Weight
        {
            public static readonly Unit Carat = new() { Symbol = "ct", Name = "Carat", Category = UnitCategory.Weight };
            public static readonly Unit Milligram = new() { Symbol = "mg", Name = "Milligram", Category = UnitCategory.Weight };
            public static readonly Unit Centigram = new() { Symbol = "cg", Name = "Centigram", Category = UnitCategory.Weight };
            public static readonly Unit Gram = new() { Symbol = "g", Name = "Gram", Category = UnitCategory.Weight };
            public static readonly Unit Hectogram = new() { Symbol = "hg", Name = "Hectogram", Category = UnitCategory.Weight };
            public static readonly Unit Kilogram = new() { Symbol = "kg", Name = "Kilogram", Category = UnitCategory.Weight };
            public static readonly Unit Tonne = new() { Symbol = "t", Name = "Tonne", Category = UnitCategory.Weight };
            public static readonly Unit Ounce = new() { Symbol = "oz", Name = "Ounce", Category = UnitCategory.Weight };
            public static readonly Unit Pound = new() { Symbol = "lb", Name = "Pound", Category = UnitCategory.Weight };
            public static readonly Unit Stone = new() { Symbol = "st", Name = "Stone", Category = UnitCategory.Weight };
        }

        // ── LENGTH ────────────────────────────────────────
        public static class Length
        {
            public static readonly Unit Nanometer = new() { Symbol = "nm", Name = "Nanometer", Category = UnitCategory.Length };
            public static readonly Unit Microns = new() { Symbol = "μm", Name = "Microns", Category = UnitCategory.Length };
            public static readonly Unit Millimeter = new() { Symbol = "mm", Name = "Millimeter", Category = UnitCategory.Length };
            public static readonly Unit Centimeter = new() { Symbol = "cm", Name = "Centimeter", Category = UnitCategory.Length };
            public static readonly Unit Meter = new() { Symbol = "m", Name = "Meter", Category = UnitCategory.Length };
            public static readonly Unit Kilometer = new() { Symbol = "km", Name = "Kilometer", Category = UnitCategory.Length };
            public static readonly Unit Inch = new() { Symbol = "in", Name = "Inch", Category = UnitCategory.Length };
            public static readonly Unit Foot = new() { Symbol = "ft", Name = "Foot", Category = UnitCategory.Length };            
            public static readonly Unit Yard = new() { Symbol = "yd", Name = "Yard", Category = UnitCategory.Length };
            public static readonly Unit Mile = new() { Symbol = "mi", Name = "Mile", Category = UnitCategory.Length };            
        }

        // ── TEMPERATURE ───────────────────────────────────
        public static class Temperature
        {
            public static readonly Unit Celsius = new() { Symbol = "°C", Name = "Celsius", Category = UnitCategory.Temperature };
            public static readonly Unit Fahrenheit = new() { Symbol = "°F", Name = "Fahrenheit", Category = UnitCategory.Temperature };
            public static readonly Unit Kelvin = new() { Symbol = "K", Name = "Kelvin", Category = UnitCategory.Temperature };
        }

        // ── SPEED ─────────────────────────────────────────
        public static class Speed
        {
            public static readonly Unit CentimeterPerSecond = new() { Symbol = "cm/s", Name = "Centimeters per second", Category = UnitCategory.Speed };
            public static readonly Unit MetersPerSecond = new() { Symbol = "m/s", Name = "Meters per second", Category = UnitCategory.Speed };
            public static readonly Unit KilometersPerHour = new() { Symbol = "km/h", Name = "Kilometers per hour", Category = UnitCategory.Speed };
            public static readonly Unit FeetPerSecond = new() { Symbol = "ft/s", Name = "Feet per second", Category = UnitCategory.Speed };
            public static readonly Unit MilesPerHour = new() { Symbol = "mph", Name = "Miles per hour", Category = UnitCategory.Speed };
            public static readonly Unit Knots = new() { Symbol = "kn", Name = "Knots", Category = UnitCategory.Speed };
            public static readonly Unit Mach = new() { Symbol = "Mach", Name = "Mach", Category = UnitCategory.Speed };
        }

        // ── VOLUME ────────────────────────────────────────
        public static class Volume
        {
            public static readonly Unit Milliliter = new() { Symbol = "ml", Name = "Milliliter", Category = UnitCategory.Volume };
            public static readonly Unit CubicCentimeter = new() { Symbol = "cm³", Name = "Cubic centimeter", Category = UnitCategory.Volume };
            public static readonly Unit Liter = new() { Symbol = "L", Name = "Liter", Category = UnitCategory.Volume };
            public static readonly Unit CubicMeter = new() { Symbol = "m³", Name = "Cubic meter", Category = UnitCategory.Volume };
            public static readonly Unit Gallon = new() { Symbol = "gal", Name = "Gallon", Category = UnitCategory.Volume };
            public static readonly Unit FluidOunce = new() { Symbol = "fl oz", Name = "Fluid ounce", Category = UnitCategory.Volume };
            public static readonly Unit CubicInch = new() { Symbol = "in³", Name = "Cubic inch", Category = UnitCategory.Volume };
            public static readonly Unit CubicFoot = new() { Symbol = "ft³", Name = "Cubic foot", Category = UnitCategory.Volume };
            public static readonly Unit CubicYard = new() { Symbol = "yd³", Name = "Cubic yard", Category = UnitCategory.Volume };
        }

        // ── TIME ──────────────────────────────────────────
        public static class Time
        {
            public static readonly Unit Nanosecond = new() { Symbol = "ns", Name = "Nanosecond", Category = UnitCategory.Time };
            public static readonly Unit Microsecond = new() { Symbol = "μs", Name = "Microsecond", Category = UnitCategory.Time };
            public static readonly Unit Millisecond = new() { Symbol = "ms", Name = "Millisecond", Category = UnitCategory.Time };
            public static readonly Unit Second = new() { Symbol = "s", Name = "Second", Category = UnitCategory.Time };
            public static readonly Unit Minute = new() { Symbol = "min", Name = "Minute", Category = UnitCategory.Time };
            public static readonly Unit Hour = new() { Symbol = "h", Name = "Hour", Category = UnitCategory.Time };
            public static readonly Unit Day = new() { Symbol = "d", Name = "Day", Category = UnitCategory.Time };
            public static readonly Unit Week = new() { Symbol = "w", Name = "Week", Category = UnitCategory.Time };
            public static readonly Unit Month = new() { Symbol = "mo", Name = "Month", Category = UnitCategory.Time };
            public static readonly Unit Year = new() { Symbol = "y", Name = "Year", Category = UnitCategory.Time };
        }

        public static class Area
        {
            public static readonly Unit SquareMillimeter = new() { Symbol = "mm²", Name = "Square millimeter", Category = UnitCategory.Area };
            public static readonly Unit SquareCentimeter = new() { Symbol = "cm²", Name = "Square centimeter", Category = UnitCategory.Area };
            public static readonly Unit SquareMeter = new() { Symbol = "m²", Name = "Square meter", Category = UnitCategory.Area };
            public static readonly Unit Hectare = new() { Symbol = "ha", Name = "Hectare", Category = UnitCategory.Area };
            public static readonly Unit SquareKilometer = new() { Symbol = "km²", Name = "Square kilometer", Category = UnitCategory.Area };
            public static readonly Unit Acre = new() { Symbol = "ac", Name = "Acre", Category = UnitCategory.Area };
            public static readonly Unit SquareInch = new() { Symbol = "in²", Name = "Square inch", Category = UnitCategory.Area };
            public static readonly Unit SquareFoot = new() { Symbol = "ft²", Name = "Square foot", Category = UnitCategory.Area };
            public static readonly Unit SquareYard = new() { Symbol = "yd²", Name = "Square yard", Category = UnitCategory.Area };
            public static readonly Unit SquareMile = new() { Symbol = "mi²", Name = "Square mile", Category = UnitCategory.Area };
        }

            // ── CURRENCY ──────────────────────────────────────
            public static class Currency
        {
            public static readonly Unit EUR = new() { Symbol = "€", Name = "Euro", Category = UnitCategory.Currency };
            public static readonly Unit USD = new() { Symbol = "$", Name = "US Dollar", Category = UnitCategory.Currency };
            public static readonly Unit GBP = new() { Symbol = "£", Name = "British Pound", Category = UnitCategory.Currency };
            public static readonly Unit JPY = new() { Symbol = "¥", Name = "Japanese Yen", Category = UnitCategory.Currency };
            public static readonly Unit BTC = new() { Symbol = "₿", Name = "Bitcoin", Category = UnitCategory.Currency };
        }
        public static class Energy
        {
            public static readonly Unit ElectronVolt = new() { Symbol = "eV", Name = "Electron Volt", Category = UnitCategory.Energy };
            public static readonly Unit Joule = new() { Symbol = "J", Name = "Joule", Category = UnitCategory.Energy };
            public static readonly Unit Kilojoule = new() { Symbol = "kJ", Name = "Kilojoule", Category = UnitCategory.Energy };
            public static readonly Unit Calorie = new() { Symbol = "cal", Name = "Calorie", Category = UnitCategory.Energy };
            public static readonly Unit Kilocalorie = new() { Symbol = "kcal", Name = "Kilocalorie", Category = UnitCategory.Energy };

        }

        public static class Power
        {
            public static readonly Unit Watt = new() { Symbol = "W", Name = "Watt", Category = UnitCategory.Power };
            public static readonly Unit Kilowatt = new() { Symbol = "kW", Name = "Kilowatt", Category = UnitCategory.Power };
            public static readonly Unit Megawatt = new() { Symbol = "MW", Name = "Megawatt", Category = UnitCategory.Power };
            public static readonly Unit Gigawatt = new() { Symbol = "GW", Name = "Gigawatt", Category = UnitCategory.Power };
            public static readonly Unit Horsepower = new() { Symbol = "hp", Name = "Horsepower", Category = UnitCategory.Power };
            public static readonly Unit BtuPerMinute = new() { Symbol = "Btu/min", Name = "BTU per Minute", Category = UnitCategory.Power };
        }

        // ── CUSTOM ────────────────────────────────────────
        public static Unit Custom(string symbol, string name) =>
        new() { Symbol = symbol, Name = name, Category = UnitCategory.Custom };

        // ── LOOKUP ────────────────────────────────────────
        public static bool FromSymbol(string symbol, [NotNullWhen(true)] out Unit? unit, UnitCategory? category = null)
        {
            var f_unit = All
                .Where(u => u.Symbol == symbol)
                .Where(u => category == null || u.Category == category)
                .FirstOrDefault();

            if(f_unit != null)
            {
                unit = f_unit;
                return true;
            }

            unit = null;
            return false;

        }

        // Tutti i valori per lookup/validazione
        public static IEnumerable<Unit> All => 
            typeof(Units)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static))
            .Where(f => f.FieldType == typeof(Unit))
            .Select(f => f.GetValue(null) as Unit)
            .Where(f => f != null).Select(f=>f);
    }
}
