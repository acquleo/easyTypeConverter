namespace easyTypeConverter.Conversion.Converter.Options
{
    public interface INumberDateTimeConverterOptions : ITypeConverterOptions
    {
        NumberDateTimeUnit Unit { get; set; }
        DateTime Epoch { get; set; }
        SerializableDateTimeKind Kind { get; set; }
    }
    public enum NumberDateTimeUnit
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
