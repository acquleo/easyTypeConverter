using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
}
