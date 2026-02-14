using easyTypeConverter.Common;
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
        readonly TypeConverter? converterHandler;
        public TypeConverterTransformer(TypeConverterTransformerOptions options):base(options)
        {
            this.options = options;
            this.converterHandler = options.Converter?.Build();
        }

        public override List<Type> SourceTypeList => new();

        protected override bool OnTransform(DataTransformContext inData, [NotNullWhen(true)] out DataTransformContext? outData)
        {
            if (this.converterHandler==null)
            {
                outData = null;
                return false;
            }
            
            var targetType = DataTypes.FromName(this.options.TargetType);
            if (!this.converterHandler.Convert(inData.Value, targetType, out var convertedData))
            {
                outData = null;
                return false;
            }

            if (convertedData == null)
            {
                outData = null;
                return false;
            }

            outData = DataTransformContext.From(convertedData, inData.ValueUnit);

            return true;
        }

    }
}
