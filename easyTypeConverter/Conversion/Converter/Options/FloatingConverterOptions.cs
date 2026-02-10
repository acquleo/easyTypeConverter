using easyTypeConverter.Conversion.Filters.Options;

namespace easyTypeConverter.Conversion.Converter.Options
{
    public class FloatingConverterOptions : ITypeConverterOptions
    {

        public List<IFilterOptions> InputFilters { get; set; } = new();

        public List<IFilterOptions> OutputFilters { get; set; } = new();
        public TypeConverter Build()
        {
            return new Converter.FloatingConverter(this);
        }
    }
}