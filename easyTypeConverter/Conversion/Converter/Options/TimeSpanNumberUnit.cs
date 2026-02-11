using System.Text.Json.Serialization;

namespace easyTypeConverter.Conversion.Converter.Options
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TimeSpanNumberUnit
    {
        Ticks,
        Seconds,
        Milliseconds,
        Minutes,
        Hours,
        Days,
        MicroSeconds
    }
}
