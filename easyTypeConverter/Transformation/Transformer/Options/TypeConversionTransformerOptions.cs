using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public class TypeConversionTransformerOptions : DataTransformerOptions
    {
        public List<ITypeConverterOptions> Options { get; set; } = new List<ITypeConverterOptions>();
        
        public override DataTransformer Build()
        {
            throw new NotImplementedException();
        }
    }
}
