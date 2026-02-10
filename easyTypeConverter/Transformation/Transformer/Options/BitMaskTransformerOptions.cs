using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public class BitMaskTransformerOptions : IDataTransformerOptions
    {
        public ulong Mask { get; set; }
        public bool Normalize { get; set; }
        public DataTransformer Build()
        {
            return new BitMaskTransformer(this);
        }
    }
}
