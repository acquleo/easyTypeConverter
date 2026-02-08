using ConsoleApp1.Conversion;
using ConsoleApp1.Conversion.Converter.Options;
using ConsoleApp1.Conversion.Converter;
using System.Globalization;
using ConsoleApp1.Conversion.Filters;
using ConsoleApp1.Conversion.Filters.Options;

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
                .AddInputFilter(new StringRemovePrefixFilterOptions()
                    .WithPrefix("0x"))
                .AddInputFilter(new StringNumericBooleanFilterOptions()
                    .WithNumberStyle(NumberStyles.HexNumber)
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

            handler.Convert("FF", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            handler.Convert("0x00", typeof(Boolean), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);

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
        public void TestStringByteConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringByteConverter(
                new StringByteConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .WithNumberStyle(NumberStyles.Any)));


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

            handler.Convert("0xFF", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)255, result);

            handler.Convert("0x00", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);

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
    }
}
