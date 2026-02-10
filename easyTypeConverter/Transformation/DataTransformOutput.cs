using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation
{
    public class DataTransformOutput
    {
        public DataTransformOutput(object  data) {
            Value = data;
        }
        public DataTransformOutput(object data, string dataUnit)
        {
            Value = data;
            ValueUnit = dataUnit;
        }
        public object Value { get; set; }
        public string? ValueUnit { get; set; }
    }
}
