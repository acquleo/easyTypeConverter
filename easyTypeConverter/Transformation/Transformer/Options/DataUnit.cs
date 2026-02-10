using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public enum DataUnit
    {
        // Bit
        Bit,

        // Byte
        Byte,

        // Base 2 (binaria) - IEC standard (1024)
        Kibibyte,   // KiB
        Mebibyte,   // MiB  
        Gibibyte,   // GiB
        Tebibyte,   // TiB
        Pebibyte,   // PiB

        // Base 10 (decimale) - SI standard (1000)
        Kilobyte,   // KB
        Megabyte,   // MB
        Gigabyte,   // GB
        Terabyte,   // TB
        Petabyte,   // PB

        // Bit base 2 (1024)
        Kibibit,    // Kibit
        Mebibit,    // Mibit
        Gibibit,    // Gibit

        // Bit base 10 (1000) - usato in networking
        Kilobit,    // kbit
        Megabit,    // Mbit
        Gigabit     // Gbit
    }
}
