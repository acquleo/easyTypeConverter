using easyTypeConverter.Common;
using easyTypeConverter.Conversion.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Converter
{
    public class StringIntegralConverter : StringIntegralConverterBase
    {
        StringNumericConverterOptions options;
        public StringIntegralConverter(StringNumericConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringIntegralConverter(): this(new StringNumericConverterOptions())
        {
            
        }

        public override List<DataType> SourceTypeList { get => new List<DataType>() { DataTypes.String }; }
        public override List<DataType> TargetTypeList { get => new List<DataType>() { DataTypes.Byte, DataTypes.SByte, DataTypes.UInt16, DataTypes.Int16, DataTypes.UInt32, DataTypes.Int32, DataTypes.UInt64, DataTypes.Int64 }; }

        static bool IsSigned(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return false;

                default:
                    throw new ArgumentException("Type is not a numeric type");
            }
        }
        public override bool OnNumericConvert(object inData, DataType targetType, [NotNullWhen(true)] out object? outData, NumberStyles numberStyle)
        {
            var culture = CultureInfoHelper.GetCultureInfo(this.options.Culture);
            outData = default;

            bool signed = IsSigned(targetType.Type);

            object? intermediateValue = null;

            if (signed)
            {
                if (!long.TryParse((string)inData, numberStyle,
                culture, out var unsignedParse))
                    return false;

                intermediateValue = unsignedParse;
            }
            else
            {
                if (!ulong.TryParse((string)inData, numberStyle,
                culture, out var signedParse))
                    return false;

                intermediateValue = signedParse;

            }


            outData = System.Convert.ChangeType(intermediateValue, targetType.Type); ;
            return true;
        }


    }
}
