using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class RangeTransformer : DataTransformer
    {
        readonly RangeTransformerOptions options;

        public RangeTransformer(RangeTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        public RangeTransformer():this(new RangeTransformerOptions())
        {
            //ELETTRICO
            //INPUT 0-10V 0-20mA
            //REGISTRO 0-65000
            // -50 + 200°C


        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        public override Type TargetType { get { return typeof(double); } }

        protected override bool OnTransform(object inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {

            var doubleValue = (double)Convert.ChangeType(inData, typeof(double));

            var normalized = (doubleValue - options.InputMin) / (options.InputMax - options.InputMin);
            outData = new DataTransformOutput(normalized * (options.OutputMax - options.OutputMin) + options.OutputMin);
            return true;
        }
    }
}
