using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class PercentageNormalizerTransformer : DataTransformer
    {
        readonly PercentageNormalizerTransformerOptions options;

        public PercentageNormalizerTransformer(PercentageNormalizerTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        public PercentageNormalizerTransformer():this(new PercentageNormalizerTransformerOptions())
        {

        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        public override Type TargetType { get { return typeof(double); } }

        protected override bool OnTransform(object inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            var doubleValue = (double)Convert.ChangeType(inData, typeof(double));

            outData = new DataTransformOutput((doubleValue - options.Min) / (options.Max - options.Min));
            return true;
        }
    }
}
