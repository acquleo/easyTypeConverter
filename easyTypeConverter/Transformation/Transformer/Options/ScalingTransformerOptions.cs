using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class ScalingTransformerOptions : IDataTransformerOptions
    {
        public double InputMin { get; set; } = 0;
        public double InputMax { get; set; } = 1.0;
        public double OutputMin { get; set; } = 0;
        public double OutputMax { get; set; } = 1.0;

        public DataTransformer Build()
        {
            return new ScalingTransformer(this);
        }
    }
}
