using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    
    public class DataUnitTransformerOptions : IDataTransformerOptions
    {
        public DataUnit SourceUnit { get; set; } = DataUnit.Byte;
        public DataUnit TargetUnit { get; set; } = DataUnit.Megabyte;

        public DataTransformer Build()
        {
            return new DataUnitTransformer();
        }
    }
}
