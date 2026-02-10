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
    /// Extract a single bit or multi bit from an integral number
    /// </summary>
    internal class BitMaskTransformer: DataTransformer
    {
        readonly BitMaskTransformerOptions options;
        readonly int _lsbPosition;
        public BitMaskTransformer(BitMaskTransformerOptions options):base(options)
        {
            this.options = options;
            _lsbPosition = ComputeLsbPosition(options.Mask);
        }

        public BitMaskTransformer():this(new  BitMaskTransformerOptions())
        {

        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) }; }
        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            var value = Convert.ToUInt64(inData.Value);
            var masked = value & options.Mask;

            // Single bit → bool
            if (IsSingleBit(options.Mask))
            {
                var val = masked != 0;
                outData =  DataTransformOutput.From(val, inData.ValueUnit);
                return true;
            }

            // Multi bit → shift to LSB if requested
            if (options.Normalize)
            { 
                var val = masked >> _lsbPosition;
                outData = DataTransformOutput.From(val, inData.ValueUnit);
                return true;
            }

            outData = DataTransformOutput.From(masked, inData.ValueUnit);
            return true;
        }

        private static bool IsSingleBit(ulong mask)
            => mask != 0 && (mask & (mask - 1)) == 0;

        private static int ComputeLsbPosition(ulong mask)
        {
            // Find position of lowest set bit
            int position = 0;
            while ((mask & 1) == 0)
            {
                mask >>= 1;
                position++;
            }
            return position;
        }
    }
}
