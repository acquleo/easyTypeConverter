using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
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

        public AutoScaleDataUnitTransformer(AutoScaleDataUnitTransformerOptions options) : base(options)
        {
            this.options = options;
            _useBinary = true;
            this._family = DetectFamily(options.SourceUnit);
        }
        public AutoScaleDataUnitTransformer():this(new AutoScaleDataUnitTransformerOptions())
        {

        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }
        public override Type TargetType { get { return typeof(double); } }

        private DataUnit[] GetScale()
        {
            return (_family, _useBinary) switch
            {
                (DataUnitFamily.Bytes, true) => _binaryByteScale,
                (DataUnitFamily.Bytes, false) => _decimalByteScale,
                (DataUnitFamily.Bits, true) => _binaryBitScale,
                (DataUnitFamily.Bits, false) => _decimalBitScale
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
        protected override bool OnTransform(object inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            var doubleValue = (double)Convert.ChangeType(inData, typeof(double));

            var scale = GetScale();
            DataUnit bestUnit = scale[0];

            var baseTransformer = new DataUnitTransformer(new DataUnitTransformerOptions().WithSourceDataUnit(options.SourceUnit).WithTargetDataUnit(bestUnit));
            if(!baseTransformer.Transform(doubleValue, out var transformedDoubleValue))
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
                if (!testTransformer.Transform(doubleValue, out var test_transformedDoubleValue))
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

            outData = new DataTransformOutput(bestValue,bestUnit.ToString());
            return true;
        }
    }
}
