using System.Globalization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IStringDateTimeConverterOptions : ITypeConverterOptions
    {
        CultureInfo Culture { get; set; }
        string[]? Formats { get; set; }
        DateTimeStyles DateTimeStyles { get; set; }
    }
}
