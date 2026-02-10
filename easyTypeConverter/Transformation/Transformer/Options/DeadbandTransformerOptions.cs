using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class DeadbandTransformerOptions : IDataTransformerOptions
    {
        public double Deadband { get; set; } = 0;

        public DataTransformer Build()
        {
            return new DeadbandTransformer(this);
        }
    }
}
