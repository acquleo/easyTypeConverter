using easyTypeConverter.Common;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    /// <summary>
    /// Automatically scales a numeric data size to the most suitable data unit.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The transformer converts the input value to the configured source unit and then
    /// selects the most appropriate unit from the available scale. The selection is based on
    /// <see cref="AutoScaleDataUnitTransformerOptions.ScaleThreshold"/> and the chosen
    /// unit family (bits or bytes) derived from <see cref="AutoScaleDataUnitTransformerOptions.SourceUnit"/>.
    /// </para>
    /// <para>
    /// The unit system (decimal or binary) is controlled by
    /// <see cref="AutoScaleDataUnitTransformerOptions.UseBinary"/>.
    /// </para>
    /// </remarks>
    public class AutoScaleDataUnitTransformer : DataTransformer
    {
        readonly AutoScaleDataUnitTransformerOptions options;
        private readonly bool _useBinary;
        private readonly DataUnitFamily _family;
        
        // Scala di unità ordinata
        private static readonly DataUnit[] _binaryByteScale =
        {
            DataUnit.Byte,
            DataUnit.Kibibyte,
            DataUnit.Mebibyte,
            DataUnit.Gibibyte,
            DataUnit.Tebibyte,
            DataUnit.Pebibyte
        };

        private static readonly DataUnit[] _decimalByteScale =
        {
            DataUnit.Byte,
            DataUnit.Kilobyte,
            DataUnit.Megabyte,
            DataUnit.Gigabyte,
            DataUnit.Terabyte,
            DataUnit.Petabyte
        };

        private static readonly DataUnit[] _binaryBitScale =
        {
            DataUnit.Bit,
            DataUnit.Kibibit,
            DataUnit.Mebibit,
            DataUnit.Gibibit
        };

        private static readonly DataUnit[] _decimalBitScale =
        {
            DataUnit.Bit,
            DataUnit.Kilobit,
            DataUnit.Megabit,
            DataUnit.Gigabit
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoScaleDataUnitTransformer"/> class
        /// using the specified options.
        /// </summary>
        /// <param name="options">
        /// Configuration options that define the source unit, scaling threshold, and binary/decimal mode.
        /// </param>
        /// <remarks>
        /// The constructor determines the unit family (bits or bytes) based on
        /// <see cref="AutoScaleDataUnitTransformerOptions.SourceUnit"/> to build the scaling list.
        /// </remarks>
        public AutoScaleDataUnitTransformer(AutoScaleDataUnitTransformerOptions options) : base(options)
        {
            this.options = options;
            _useBinary = options.UseBinary;
            this._family = DetectFamily(options.SourceUnit);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoScaleDataUnitTransformer"/> class
        /// with default options.
        /// </summary>
        /// <remarks>
        /// This overload uses a new <see cref="AutoScaleDataUnitTransformerOptions"/> instance
        /// with its default values.
        /// </remarks>
        public AutoScaleDataUnitTransformer():this(new AutoScaleDataUnitTransformerOptions())
        {

        }

        /// <summary>
        /// Gets the list of numeric types supported by this transformer.
        /// </summary>
        /// <value>
        /// A list containing integral and floating-point types: <see cref="byte"/>, <see cref="sbyte"/>,
        /// <see cref="ushort"/>, <see cref="short"/>, <see cref="uint"/>, <see cref="int"/>,
        /// <see cref="ulong"/>, <see cref="long"/>, <see cref="float"/>, and <see cref="double"/>.
        /// </value>
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }
        
        private DataUnit[] GetScale()
        {
            return (_family, _useBinary) switch
            {
                (DataUnitFamily.Bytes, true) => _binaryByteScale,
                (DataUnitFamily.Bytes, false) => _decimalByteScale,
                (DataUnitFamily.Bits, true) => _binaryBitScale,
                (DataUnitFamily.Bits, false) => _decimalBitScale,
                    _ => throw new InvalidOperationException("Invalid data unit family or binary/decimal choice.")

            };
        }
        private static DataUnitFamily DetectFamily(DataUnit unit)
        {
            return unit switch
            {
                DataUnit.Bit or DataUnit.Kilobit or DataUnit.Megabit or DataUnit.Gigabit
                    or DataUnit.Kibibit or DataUnit.Mebibit or DataUnit.Gibibit
                    => DataUnitFamily.Bits,
                _ => DataUnitFamily.Bytes
            };
        }

        /// <summary>
        /// Converts a <see cref="DataUnit"/> value to its corresponding <see cref="Unit"/> instance.
        /// </summary>
        /// <param name="dataUnit">The data unit to convert.</param>
        /// <returns>
        /// A <see cref="Unit"/> instance describing the specified <paramref name="dataUnit"/>.
        /// </returns>
        /// <remarks>
        /// If the unit is not explicitly mapped, a fallback <see cref="Unit"/> is created using
        /// the enum name as both symbol and display name.
        /// </remarks>
        public static Unit GetUnit(DataUnit dataUnit)
        {
            switch (dataUnit)
            {
                case DataUnit.Bit:
                    return Units.Data.Bit;
                case DataUnit.Byte:
                    return Units.Data.Byte;
                case DataUnit.Kilobit:
                    return Units.Data.Kilobit;
                case DataUnit.Kilobyte:
                    return Units.Data.Kilobyte;
                case DataUnit.Megabit:
                    return Units.Data.Megabit;
                case DataUnit.Megabyte:
                    return Units.Data.Megabyte;
                case DataUnit.Gigabit:
                    return Units.Data.Gigabit;
                case DataUnit.Gigabyte:
                    return Units.Data.Gigabyte;
                case DataUnit.Terabyte:
                    return Units.Data.Terabyte;
                case DataUnit.Petabyte:
                    return Units.Data.Petabyte;
                case DataUnit.Kibibit:
                    return Units.Data.Kibibit;
                case DataUnit.Kibibyte:
                    return Units.Data.Kibibyte;
                case DataUnit.Mebibit:
                    return Units.Data.Mebibit;
                case DataUnit.Mebibyte:
                    return Units.Data.Mebibyte;
            }

            return new Unit { Symbol = dataUnit.ToString(), Name = dataUnit.ToString(), Category = UnitCategory.Data, IsBinary = false };
        }

        /// <summary>
        /// Scales the input value to the most suitable unit based on the configured threshold.
        /// </summary>
        /// <param name="inData">
        /// The input value and optional unit. If the unit is null, the configured source unit is used.
        /// </param>
        /// <param name="outData">
        /// When this method returns, contains the scaled value and unit.
        /// </param>
        /// <returns>
        /// <c>true</c> when the transformation succeeds; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The method converts the source value to the base unit of the selected scale, then iteratively
        /// evaluates higher units until the value drops below
        /// <see cref="AutoScaleDataUnitTransformerOptions.ScaleThreshold"/>.
        /// </remarks>
        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            var unit = inData.ValueUnit == null ? GetUnit(options.SourceUnit) : inData.ValueUnit;
            var doubleValue = (double)Convert.ChangeType(inData.Value, typeof(double));

            var scale = GetScale();
            DataUnit bestUnit = scale[0];

            var baseTransformer = new DataUnitTransformer(new DataUnitTransformerOptions().WithSourceDataUnit(options.SourceUnit).WithTargetDataUnit(bestUnit));
            if(!baseTransformer.Transform(DataTransformOutput.From(doubleValue, unit), out var transformedDoubleValue))
            {
                outData = null;
                return false;
            }

            if(transformedDoubleValue?.Value.GetType()!=typeof(double))
            {
                outData = null;
                return false;
            }

            double bestValue = (double)transformedDoubleValue.Value;

            for (int i = 1; i < scale.Length; i++)
            {
                var testTransformer = new DataUnitTransformer(new DataUnitTransformerOptions().WithSourceDataUnit(scale[0]).WithTargetDataUnit(scale[i]));
                if (!testTransformer.Transform(DataTransformOutput.From(doubleValue, unit), out var test_transformedDoubleValue))
                {
                    outData = null;
                    return false;
                }

                if (test_transformedDoubleValue?.Value.GetType() != typeof(double))
                {
                    outData = null;
                    return false;
                }

                double testValue = (double)test_transformedDoubleValue.Value;

                // Scala quando raggiungi il threshold (es. 1.0, 0.9, 1024, ecc.)
                if (testValue >= options.ScaleThreshold)
                {
                    bestUnit = scale[i];
                    bestValue = testValue;
                }
                else
                {
                    break;
                }
            }

            outData = DataTransformOutput.From(bestValue, GetUnit(bestUnit));
            return true;
        }
    }
}
