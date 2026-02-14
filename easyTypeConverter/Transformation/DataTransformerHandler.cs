using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Transformation;
using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public class DataTransformerHandler
    {
        private List<DataTransformer> transformers = new();

        public DataTransformerHandler()
        {


        }

        public void AddTransformer(DataTransformerOptions options)
        {
            var dataTransformer = options.Build();
            transformers.Add(dataTransformer);            
        }

        public bool Transform(DataTransformContext? inData, out DataTransformContext? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            DataTransformContext? currrentOutput = inData;

            foreach (var converter in transformers)
            {
                try
                {
                    if (!converter.Transform(currrentOutput, out var outConverterData))
                        continue;

                    currrentOutput = outConverterData;
                }
                catch
                {
                    throw;
                }
            }       
            
            outData = currrentOutput;
            return true;
        }
    }
}
