using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class DeadbandTransformer : DataTransformer
    {
        DeadbandTransformerOptions options;
        private object? _lastValue;
        public DeadbandTransformer(DeadbandTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        public DeadbandTransformer() : this(new DeadbandTransformerOptions())
        {

        }   

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            // First value always passes through
            if (_lastValue is null)
            {
                _lastValue = inData.Value;
                outData = inData;
                return true;
            }

            var delta = ComputeDelta(inData.Value, _lastValue);

            if (delta > options.Deadband)
            {
                _lastValue = inData.Value;
                outData = inData;
                return true;
            }

            outData = DataTransformOutput.From(_lastValue, inData.ValueUnit);
            return true;
        }

        private double ComputeDelta(object current, object last)
        {
            // Convert both to double to avoid overflow during subtraction
            var currentDouble = Convert.ToDouble(current);
            var lastDouble = Convert.ToDouble(last);

            return Math.Abs(currentDouble - lastDouble);
        }
    }
}
