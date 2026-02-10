using easyTypeConverter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public class DataTransformOutput
    {
        public static DataTransformOutput FromOutput(DataTransformOutput output)
        {
            return new DataTransformOutput(output.Value, output.ValueUnit);
        }

        public static DataTransformOutput From(object data, Unit? dataUnit)
        {
            return new DataTransformOutput(data, dataUnit);
        }
        public static DataTransformOutput From(object data)
        {
            return new DataTransformOutput(data);
        }
        private DataTransformOutput(object  data) {
            Value = data;
        }
        private DataTransformOutput(object data, Unit? dataUnit)
        {
            Value = data;
            ValueUnit = dataUnit;
        }
        public object Value { get; set; }
        public Unit? ValueUnit { get; set; }
    }
}
