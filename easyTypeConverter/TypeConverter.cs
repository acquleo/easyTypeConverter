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
        HashSet<Type> targetTypes = new HashSet<Type> ();
        HashSet<Type> sourceTypes = new HashSet<Type>();

        public TypeConverter(ITypeConverterOptions options)
        {
            this.options = options;
            foreach (var filter in this.options.InputFilters)
                this.AddInputFilter(filter.Build());
            foreach (var filter in this.options.OutputFilters)
                this.AddOutputFilter(filter.Build());

            foreach(var type in TargetTypeList)
            {
                if(!targetTypes.Contains(type))
                    targetTypes.Add(type);
            }

            foreach (var type in SourceTypeList)
            {
                if (!sourceTypes.Contains(type))
                    sourceTypes.Add(type);
            }
        }

        protected void AddInputFilter(Filter inputFilter)
        {
            inputFilters.Add(inputFilter);
        }

        protected void AddOutputFilter(Filter outputFilter)
        {
            outputFilters.Add(outputFilter);
        }

        public abstract List<Type> SourceTypeList { get; }
        public abstract List<Type> TargetTypeList { get; }
        public abstract bool OnConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData);
        public bool Convert(object? inData, out object? outData)
        {
            var targetType = TargetTypeList.FirstOrDefault();
            if (targetType == null)
                throw new InvalidOperationException();

            return Convert(inData, targetType, out outData);
        }

        public bool Convert(object? inData, Type targetType, out object? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            if (!sourceTypes.Contains(inData.GetType()))
            {
                outData = null;
                return false;
            }

            if (!targetTypes.Contains(targetType))
            {
                outData = null;
                return false;
            }

            foreach (var filter in inputFilters)
            {
                var result = filter.Process(inData, out inData);
                if (result == FilterAction.Exit) break;
            }            

            if(!OnConvert(inData, targetType, out outData))
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
