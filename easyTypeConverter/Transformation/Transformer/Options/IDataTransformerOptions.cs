using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public interface IDataTransformerOptions
    {
        DataTransformer Build();
    }
}
