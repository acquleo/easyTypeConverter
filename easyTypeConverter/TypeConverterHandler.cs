using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter
{
    public class TypeConverterHandler
    {
        private ConcurrentDictionary<Tuple<Type, Type>, List<TypeConverter>> converters = new();
        private ConcurrentDictionary<TypeConverter, Delegate> _cache = new();

        public TypeConverterHandler()
        {


        }

        public void AddConverter(TypeConverter typeConverter)
        {
            var key = new Tuple<Type, Type>(typeConverter.SourceType, typeConverter.TargetType);
            if (!converters.ContainsKey(key))
            {
                converters.TryAdd(key, new List<TypeConverter>());
            }

            converters[key].Add(typeConverter);
        }

        bool FindConverter(Type inType, Type outType, [NotNullWhen(true)] out List<TypeConverter>? typeConverters)
        {
            var key = new Tuple<Type, Type>(inType, outType);
            return converters.TryGetValue(key, out typeConverters);
        }

        Func<object?, (bool, object?)> GetFunction(TypeConverter converter, Type inType, Type outType)
        {
            if (!_cache.TryGetValue(converter, out var del))
            {
                var convType = converter.GetType();
                var method = convType.GetMethod("Convert")!;

                // parametri lambda: object? input
                var inParam = Expression.Parameter(typeof(object), "inData");

                // variabile per outData
                var outVar = Expression.Variable(typeof(object), "outData");

                // chiamata Convert
                var call = Expression.Call(Expression.Constant(converter), method, inParam, outVar);

                // corpo lambda: restituisce tuple (bool, object?)
                var body = Expression.New(
                    typeof(ValueTuple<bool, object>).GetConstructor(new[] { typeof(bool), typeof(object) })!,
                    call,
                    outVar
                );

                // Blocco che include la variabile e ritorna la tupla
                var block = Expression.Block(
                    new[] { outVar },  // variabili locali
                    body          // espressione da ritornare
                );

                // Creazione della lambda
                var lambda = Expression.Lambda<Func<object?, (bool, object?)>>(
                    block,
                    inParam
                );

                del = lambda.Compile();
                _cache[converter] = del;
            }
            return (Func<object?, (bool, object?)>)del;
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
                var function = GetFunction(converter, inType, outType);
                try
                {
                    var result = function(inData);

                    if (!result.Item1)
                        continue;

                    outData = result.Item2;
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
