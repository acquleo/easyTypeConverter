using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class DataUnitTransformer : DataTransformer
    {
        // Tabella di conversione a byte (unità base)
        private static readonly Dictionary<DataUnit, double> _toBytesMultiplier = new()
        {
            // Bit base
            [DataUnit.Bit] = 1.0 / 8.0,

            // Byte base
            [DataUnit.Byte] = 1.0,

            // BASE 2 (1024) - Binario - IEC standard
            [DataUnit.Kibibyte] = Math.Pow(1024, 1),      // 1024 B
            [DataUnit.Mebibyte] = Math.Pow(1024, 2),      // 1,048,576 B
            [DataUnit.Gibibyte] = Math.Pow(1024, 3),      // 1,073,741,824 B
            [DataUnit.Tebibyte] = Math.Pow(1024, 4),
            [DataUnit.Pebibyte] = Math.Pow(1024, 5),

            // BASE 10 (1000) - Decimale - SI standard
            [DataUnit.Kilobyte] = Math.Pow(1000, 1),      // 1000 B
            [DataUnit.Megabyte] = Math.Pow(1000, 2),      // 1,000,000 B
            [DataUnit.Gigabyte] = Math.Pow(1000, 3),      // 1,000,000,000 B
            [DataUnit.Terabyte] = Math.Pow(1000, 4),
            [DataUnit.Petabyte] = Math.Pow(1000, 5),

            // Bit BASE 2 (1024)
            [DataUnit.Kibibit] = Math.Pow(1024, 1) / 8.0,
            [DataUnit.Mebibit] = Math.Pow(1024, 2) / 8.0,
            [DataUnit.Gibibit] = Math.Pow(1024, 3) / 8.0,

            // Bit BASE 10 (1000) - networking standard
            [DataUnit.Kilobit] = Math.Pow(1000, 1) / 8.0,
            [DataUnit.Megabit] = Math.Pow(1000, 2) / 8.0,
            [DataUnit.Gigabit] = Math.Pow(1000, 3) / 8.0
        };
        readonly DataUnitTransformerOptions options;
        public DataUnitTransformer(DataUnitTransformerOptions options) : base(options)
        {
            this.options = options;
        }
        public DataUnitTransformer():this(new DataUnitTransformerOptions())
        {

        }
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }
        protected override bool OnTransform(DataTransformContext inData, [NotNullWhen(true)] out DataTransformContext? outData)
        {
            var doubleValue = (double)Convert.ChangeType(inData.Value, typeof(double));

            // Converti a byte, poi all'unità target
            var bytes = doubleValue * _toBytesMultiplier[options.SourceUnit];
            var outValue = bytes / _toBytesMultiplier[options.TargetUnit];
            outData = DataTransformContext.From(outValue, AutoScaleDataUnitTransformer.GetUnit(options.TargetUnit));
            return true;
        }
    }
}
