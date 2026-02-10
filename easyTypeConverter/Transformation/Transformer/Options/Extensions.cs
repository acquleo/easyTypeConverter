using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer.Options
{
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
}
