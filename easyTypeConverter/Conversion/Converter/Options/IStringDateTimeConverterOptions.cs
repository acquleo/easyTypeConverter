using System.Globalization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringDateTimeConverterOptions 
    {
        public string Culture { get; set; }
        string[]? Formats { get; set; }
        DateTimeStyles DateTimeStyles { get; set; }
    }
}
