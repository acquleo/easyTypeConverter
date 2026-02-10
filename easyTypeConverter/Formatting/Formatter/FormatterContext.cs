using easyTypeConverter.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Formatting.Formatter
{
    public class FormatterContext
    {
        public static FormatterContext From(object data, Unit? dataUnit = null, string? status = null, DateTime? timestamp = null)
        {
            return new FormatterContext(data, dataUnit, status, timestamp);
        }
        public static FormatterContext From(object data)
        {
            return new FormatterContext(data);
        }
        private FormatterContext(object data, Unit? dataUnit = null, string? status=null,DateTime? timestamp=null)
        {
            Value = data;
            ValueUnit = dataUnit;
            Status = status;
            Timestamp = timestamp;
        }
        public object Value { get; set; }
        public Unit? ValueUnit { get; set; }
        public string? Status { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
