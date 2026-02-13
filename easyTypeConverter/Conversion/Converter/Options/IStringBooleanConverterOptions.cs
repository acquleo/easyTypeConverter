using System.Collections.Generic;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringBooleanConverterOptions 
    {
        bool DefaultOutput { get; set; }
        List<string> TrueValues { get; set; }
        List<string> FalseValues { get; set; }
    }
}
