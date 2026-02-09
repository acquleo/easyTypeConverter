using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace easyTypeConverter
{
    public abstract class TypeConverter
    {
        private ITypeConverterOptions options;
        private List<Filter> inputFilters = new();
        private List<Filter> outputFilters = new ();
        public TypeConverter(ITypeConverterOptions options)
        {
            this.options = options;
            foreach (var filter in this.options.InputFilters)
                this.AddInputFilter(filter.Build());
            foreach (var filter in this.options.OutputFilters)
                this.AddOutputFilter(filter.Build());
        }

        protected void AddInputFilter(Filter inputFilter)
        {
            inputFilters.Add(inputFilter);
        }

        protected void AddOutputFilter(Filter outputFilter)
        {
            outputFilters.Add(outputFilter);
        }

        public abstract Type SourceType { get; }
        public abstract Type TargetType { get; }
        public abstract bool OnConvert(object inData, [NotNullWhen(true)] out object? outData);
        public bool Convert(object? inData, out object? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            foreach (var filter in inputFilters)
            {
                var result = filter.Process(inData, out inData);
                if (result == FilterAction.Exit) break;
            }

            if (inData.GetType() != SourceType)
            {
                outData = null;
                return false;
            }

            if(!OnConvert(inData, out outData))
            {
                outData = null;
                return false;
            }

            foreach (var filter in outputFilters)
            {
                var result = filter.Process(outData, out outData);
                if (result == FilterAction.Exit) break;
            }

            return true;
        }
    }
}
