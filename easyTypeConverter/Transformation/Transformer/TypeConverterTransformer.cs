using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    public class TypeConverterTransformer : DataTransformer
    {
        readonly TypeConverterTransformerOptions options;
        readonly ITypeConverterOptions converterHandler;
        public TypeConverterTransformer(TypeConverterTransformerOptions options):base(options)
        {
            this.options = options;
            //this.converterHandler = options.Build();
        }

        public override List<Type> SourceTypeList => new();

        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            throw new NotImplementedException();
            //var targetType = Type.GetType(options.TargetType) ?? throw new Exception($"Invalid target type: {options.TargetType}");

            //if (!this.converterHandler.Convert(inData.Value, targetType, out var convertedData))
            //{
            //    outData = null;
            //    return false;
            //}
            
            //if(convertedData == null)
            //{
            //    outData = null;
            //    return false;
            //}

            //outData = DataTransformOutput.From(convertedData, inData.ValueUnit);

            //return true;
        }

    }
}
