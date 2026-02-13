using easyTypeConverter.Common;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace easyTypeConverter.Conversion
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
                if(!targetTypes.Contains(type.Type))
                    targetTypes.Add(type.Type);
            }

            foreach (var type in SourceTypeList)
            {
                if (!sourceTypes.Contains(type.Type))
                    sourceTypes.Add(type.Type);
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

        protected bool IsSourceType(Type type)
        { return sourceTypes.Contains(type); }
        protected bool IsTargetType(Type type)
        { return targetTypes.Contains(type); }

        public abstract List<DataType> SourceTypeList { get; }
        public abstract List<DataType> TargetTypeList { get; }
        public abstract bool OnConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData);
        public bool Convert(object? inData, out object? outData)
        {
            var targetType = TargetTypeList.FirstOrDefault();
            if (targetType == null)
                throw new TargetTypeException(this);

            return Convert(inData, targetType, out outData);
        }
        public bool Convert(object? inData, DataType targetType, out object? outData)
        {
            if (inData == null)
            {
                outData = null;
                return true;
            }

            if (!IsSourceType(inData.GetType()))
            {
                outData = null;
                return false;
            }

            if (!IsTargetType(targetType.Type))
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
