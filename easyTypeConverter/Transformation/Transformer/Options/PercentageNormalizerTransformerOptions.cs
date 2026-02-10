using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class PercentageNormalizerTransformerOptions : IDataTransformerOptions
    {
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 1.0;
        public DataTransformer Build()
        {
            return new PercentageNormalizerTransformer();
        }
    }
}
