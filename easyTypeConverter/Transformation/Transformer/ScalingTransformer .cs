using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class ScalingTransformer : DataTransformer
    {
        readonly ScalingTransformerOptions options;

        public ScalingTransformer (ScalingTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        public ScalingTransformer ():this(new ScalingTransformerOptions())
        {

        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {

            var doubleValue = (double)Convert.ChangeType(inData.Value, typeof(double));

            var normalized = (doubleValue - options.InputMin) / (options.InputMax - options.InputMin);
            var outValue = normalized * (options.OutputMax - options.OutputMin) + options.OutputMin;
            outData = DataTransformOutput.From(outValue, inData.ValueUnit);
            return true;
        }
    }
}
