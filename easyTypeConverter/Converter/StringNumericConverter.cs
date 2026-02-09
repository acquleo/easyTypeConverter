using easyTypeConverter.Converter.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Converter
{
    public class StringNumericConverter : StringNumericConverterBase
    {
        StringNumericConverterOptions options;
        public StringNumericConverter(StringNumericConverterOptions options) : base(options)
        {
            this.options = options;

        }

        public StringNumericConverter(): this(new StringNumericConverterOptions())
        {
            
        }

        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(string) }; }
        public override List<Type> TargetTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) }; }

        bool IsSigned(Type type)
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
        public override bool OnNumericConvert(object inData, Type targetType, [NotNullWhen(true)] out object? outData, NumberStyles numberStyle)
        {
            outData = default;

            bool signed = IsSigned(targetType);

            object? intermediateValue = null;

            if (signed)
            {
                if (!long.TryParse((string)inData, numberStyle,
                this.options.Culture, out var unsignedParse))
                    return false;

                intermediateValue = unsignedParse;
            }
            else
            {
                if (!ulong.TryParse((string)inData, numberStyle,
                this.options.Culture, out var signedParse))
                    return false;

                intermediateValue = signedParse;

            }


            outData = System.Convert.ChangeType(intermediateValue, targetType); ;
            return true;
        }


    }
}
