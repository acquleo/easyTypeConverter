using easyTypeConverter.Transformation.Exceptions;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{        
    public abstract class DataTransformer
    {
        readonly HashSet<Type> sourceTypes = new HashSet<Type>();
        readonly DataTransformerOptions options;
        public DataTransformer(DataTransformerOptions options)
        {
            this.options = options;
            
            foreach (var type in SourceTypeList)
            {
                if (!sourceTypes.Contains(type))
                    sourceTypes.Add(type);
            }
        }
        protected bool IsSourceType(Type type) 
        { 
            if(sourceTypes.Count==0)
                return true; // If no source types defined, accept all types

            return sourceTypes.Contains(type); 
        }

        public abstract List<Type> SourceTypeList { get; }
        protected abstract bool OnTransform(DataTransformContext inData, [NotNullWhen(true)] out DataTransformContext? outData);
        public bool Transform(DataTransformContext? inData, out DataTransformContext? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            var sourceType = inData.Value.GetType();

            if (!IsSourceType(sourceType))
            {
                throw new SourceTypeTransformException(this, sourceType);
            }

            try
            {
                return OnTransform(inData, out outData);
            }
            catch (Exception ex)
            {
                throw new DataTransformException(this, inData, sourceType, ex);
            }            
        }
    }
}
