using easyTypeConverter;
using easyTypeConverter.Converter.Options;
using easyTypeConverter.Converter;
using System.Globalization;
using easyTypeConverter.Filters;
using easyTypeConverter.Filters.Options;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        //TypeConverterHandler handler = new TypeConverterHandler();

        public Test1()
        {
            
        }
        
        [TestMethod]
        public void TestStringBooleanConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringBooleanConverter(
                new StringBooleanConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())                
                .AddInputFilter(new StringNumericBooleanFilterOptions()
                    .WithNumberStyle(NumberStyles.Any)
                    .WithExitOnMatch())
                .AddInputFilter(new StringNumericBooleanFilterOptions()
                    .WithExitOnMatch())
                .AddInputFilter(new StringReplaceFilterOptions()
                    .WithReplace("True","on","ACTIVE")
                    .WithReplace("False", "off", "DEACTIVE")
                    .WithExitOnMatch())
                .AddInputFilter(new StringRegexMatchReplaceFilterOptions()
                    .WithReplace("True", ".* ACTIVE")
                    .WithReplace("False", ".* DEACTIVE")
                    .WithExitOnMatch())));


            object? result = null;
            handler.Convert("0", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);

            handler.Convert("1", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            handler.Convert(" 1", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            handler.Convert("12", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("FF", typeof(Boolean), out result);
            });


            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("0x00", typeof(Boolean), out result);
            });

            handler.Convert("-4", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("pippo", typeof(bool), out result);
            });

            handler.Convert("True", typeof(bool), out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestStringByteHexConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringByteConverter(
                new StringByteConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .AddInputFilter(new StringRemovePrefixFilterOptions()
                    .WithPrefix("0x"))
              .WithNumberStyle(NumberStyles.HexNumber)));


            object? result = null;
            handler.Convert("0", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            handler.Convert("1", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)1, result);

            handler.Convert("12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("0xFF", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)255, result);

            handler.Convert("0x00", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("-4", typeof(byte), out result);
            });
            

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("pippo", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("True", typeof(byte), out result);
            });

        }

        [TestMethod]
        public void TestStringByteHexAutodetectConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringByteConverter(
                new StringByteConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .WithHexDetection()
                .WithAddPrefix("HEXAAA!")));


            object? result = null;
            handler.Convert("0", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            handler.Convert("1", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)1, result);

            handler.Convert("12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)12, result);

            handler.Convert("0x12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("&h12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("0X12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);


            handler.Convert("HEXAAA!FA", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)250, result);
        }
        [TestMethod]
        public void TestStringByteConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringByteConverter(
                new StringByteConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())));


            object? result = null;
            handler.Convert("0", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            handler.Convert("1", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)1, result);

            handler.Convert("12", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)12, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("0xFF", typeof(byte), out result);
            });


            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("0x00", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("-4", typeof(byte), out result);
            });
            
            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("pippo", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("True", typeof(byte), out result);
            });

            
        }

        [TestMethod]
        public void TestStringSByteConverter()
        {
            var outType = typeof(sbyte);

            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringSByteConverter(
                new StringSByteConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())));


            object? result = null;
            handler.Convert("0", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)0, result);

            handler.Convert("1", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)1, result);

            handler.Convert("12", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)12, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("0xFF", outType, out result);
            });


            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("0x00", outType, out result);
            });

            handler.Convert("-4", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)-4, result);

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("pippo", outType, out result);
            });

            Assert.ThrowsException<TypeConverterFailedException>(() =>
            {
                handler.Convert("True", outType, out result);
            });


        }
    }
}
