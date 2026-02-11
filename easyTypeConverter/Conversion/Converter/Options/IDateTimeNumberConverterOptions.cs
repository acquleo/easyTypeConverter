using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface IDateTimeNumberConverterOptions : ITypeConverterOptions
    {
        DateTimeNumberUnit Unit { get; set; }
        DateTime Epoch { get; set; }
    }
    public enum DateTimeNumberUnit
    {
        Ticks,
        Seconds,
        Milliseconds,
        Microseconds,
        Minutes,
        Hours,
        Days,
        OADate
    }
}
