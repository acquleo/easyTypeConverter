using easyTypeConverter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public class DataTransformContext
    {
        public static DataTransformContext FromOutput(DataTransformContext output)
        {
            return new DataTransformContext(output.Value, output.ValueUnit);
        }

        public static DataTransformContext From(object data, Unit? dataUnit)
        {
            return new DataTransformContext(data, dataUnit);
        }
        public static DataTransformContext From(object data)
        {
            return new DataTransformContext(data);
        }
        private DataTransformContext(object  data) {
            Value = data;
        }
        private DataTransformContext(object data, Unit? dataUnit)
        {
            Value = data;
            ValueUnit = dataUnit;
        }
        public object Value { get; set; }
        public Unit? ValueUnit { get; set; }
    }
}
