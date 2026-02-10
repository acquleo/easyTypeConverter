using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public abstract class DataTransformer
    {
        readonly HashSet<Type> sourceTypes = new HashSet<Type>();
        readonly IDataTransformerOptions options;
        public DataTransformer(IDataTransformerOptions options)
        {
            this.options = options;
            
            foreach (var type in SourceTypeList)
            {
                if (!sourceTypes.Contains(type))
                    sourceTypes.Add(type);
            }
        }
        protected bool IsSourceType(Type type)
        { return sourceTypes.Contains(type); }

        public abstract List<Type> SourceTypeList { get; }
        protected abstract bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData);
        public bool Transform(DataTransformOutput? inData, out DataTransformOutput? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            if (!IsSourceType(inData.Value.GetType()))
            {
                outData = null;
                return false;
            }

            return OnTransform(inData, out outData);
        }
    }
}
