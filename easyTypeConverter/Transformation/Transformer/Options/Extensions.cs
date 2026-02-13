using easyTypeConverter.Common;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
    public static class TypeConverterTransformerOptionsExtensions
    {
        public static T WithConverter<T>(this T obj, ITypeConverterOptions options)
               where T : TypeConverterTransformerOptions
        {
            obj.Converter = options;
            return obj;
        }

        public static T WithTargetType<T>(this T obj, string targetType)
              where T : TypeConverterTransformerOptions
        {
            obj.TargetType = targetType;
            return obj;
        }

        public static T WithTargetType<T>(this T obj, DataType targetType)
              where T : TypeConverterTransformerOptions
        {
            obj.TargetType = targetType.Name;
            return obj;
        }
    }
    public static class DataTransformerHandlerOptionsExtensions
    {
        public static T AddTransformer<T>(this T obj, DataTransformerOptions options)
               where T : DataTransformerHandlerOptions
        {
            obj.Transformers.Add(options);
            return obj;
        }

    }
    public static class DataUnitTransformerOptionsExtensions
    {
        public static T WithSourceDataUnit<T>(this T obj, DataUnit unit)
               where T : DataUnitTransformerOptions
        {
            obj.SourceUnit = unit;
            return obj;
        }

        public static T WithTargetDataUnit<T>(this T obj, DataUnit unit)
               where T : DataUnitTransformerOptions
        {
            obj.TargetUnit = unit;
            return obj;
        }
    }

    public static class AutoScaleDataUnitTransformerOptionsExtensions
    {
        public static T WithSourceDataUnit<T>(this T obj, DataUnit unit)
               where T : AutoScaleDataUnitTransformerOptions
        {
            obj.SourceUnit = unit;
            return obj;
        }

        public static T WithScaleThreshold<T>(this T obj, double treshold)
               where T : AutoScaleDataUnitTransformerOptions
        {
            obj.ScaleThreshold = treshold;
            return obj;
        }

        public static T WithUseBinary<T>(this T obj, bool binary = true)
               where T : AutoScaleDataUnitTransformerOptions
        {
            obj.UseBinary= binary;
            return obj;
        }
        public static T WithDecimal<T>(this T obj)
               where T : AutoScaleDataUnitTransformerOptions
        {
            obj.UseBinary = false;
            return obj;
        }

    }

    public static class RangeTransformerOptionsExtensions
    {
        public static T WithOutputMin<T>(this T obj, double value)
               where T : ScalingTransformerOptions
        {
            obj.OutputMin = value;
            return obj;
        }

        public static T WithOutputMax<T>(this T obj, double value)
               where T : ScalingTransformerOptions
        {
            obj.OutputMax = value;
            return obj;
        }

        public static T WithInputMin<T>(this T obj, double value)
               where T : ScalingTransformerOptions
        {
            obj.InputMin = value;
            return obj;
        }

        public static T WithInputMax<T>(this T obj, double value)
               where T : ScalingTransformerOptions
        {
            obj.InputMax = value;
            return obj;
        }
    }

    public static class DeadbandTransformerOptionsExtensions
    {
        public static T WithDeadband<T>(this T obj, double deadband)
               where T : DeadbandTransformerOptions
        {
            obj.Deadband = deadband;
            return obj;
        }

    }

    public static class BitMaskTransformerOptionsExtensions
    {
        public static T WithMask<T>(this T obj, ulong mask)
               where T : BitMaskTransformerOptions
        {
            obj.Mask = mask;
            return obj;
        }
        public static T WithNormalize<T>(this T obj, bool normalize = true)
               where T : BitMaskTransformerOptions
        {
            obj.Normalize = normalize;
            return obj;
        }
    }
}
