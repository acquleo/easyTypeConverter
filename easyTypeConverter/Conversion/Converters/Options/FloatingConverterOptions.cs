using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converters.Options
{
    public class FloatingConverterOptions : ITypeConverterOptions
    {
        public List<IFilterOptions> InputFilters { get; set; } = new();
        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converters.FloatingConverter(this);
        }
    }
}