using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion
{
    public class TypeConverterHandler
    {
        private ConcurrentDictionary<Tuple<Type, Type>, List<TypeConverter>> converters = new();
        
        public TypeConverterHandler()
        {


        }

        public void AddConverter(ITypeConverterOptions options)
        {
            var typeConverter = options.Build();

            foreach (var sourceType in typeConverter.SourceTypeList)
            {
                foreach(var targetType in typeConverter.TargetTypeList)
                {
                    var key = new Tuple<Type, Type>(sourceType, targetType);
                    if (!converters.ContainsKey(key))
                    {
                        converters.TryAdd(key, new List<TypeConverter>());
                    }

                    converters[key].Add(typeConverter);
                }
            }
            
        }

        bool FindConverter(Type inType, Type outType, [NotNullWhen(true)] out List<TypeConverter>? typeConverters)
        {
            var key = new Tuple<Type, Type>(inType, outType);
            return converters.TryGetValue(key, out typeConverters);
        }

        public bool CanConvert(object inData, Type outType)
        {
            if (inData == null) return true;

            Type inType = inData.GetType();

            return FindConverter(inType, outType, out var _);
        }

        public bool Convert(object? inData, Type outType, out object? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            Type inType = inData.GetType();

            if (!FindConverter(inType, outType, out var typeConverters))
            {
                throw new TypeConverterNotFoundException(inData, inType, outType);
            }

            foreach (var converter in typeConverters)
            {
                try
                {
                    if (!converter.Convert(inData, outType, out var outConverterData))
                        continue;

                    outData = outConverterData;
                    return true;
                }
                catch (Exception ex)
                {
                    throw new TypeConverterException(inData, inType, outType, ex);
                }
            }

            throw new TypeConverterFailedException(inData, inType, outType);
        }
    }
}
