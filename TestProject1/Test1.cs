using System.Globalization;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Transformation.Transformer.Options;
using easyTypeConverter.Transformation;

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
            handler.AddConverter(new StringBooleanConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .AddInputFilter(new StringNumericBooleanFilterOptions()
                    .WithNumberStyle(NumberStyles.Any)
                    .WithExitOnMatch())
                .AddInputFilter(new StringNumericBooleanFilterOptions()
                    .WithExitOnMatch())
                .AddInputFilter(new StringReplaceFilterOptions()
                    .WithReplace("True", "on", "ACTIVE")
                    .WithReplace("False", "off", "DEACTIVE")
                    .WithExitOnMatch())
                .AddInputFilter(new StringRegexMatchReplaceFilterOptions()
                    .WithReplace("True", ".* ACTIVE")
                    .WithReplace("False", ".* DEACTIVE")
                    .WithExitOnMatch()));


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
        public void TestStringNumericHexConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringNumericConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .AddInputFilter(new StringRemovePrefixFilterOptions()
                    .WithPrefix("0x"))
              .WithNumberStyle(NumberStyles.HexNumber));


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

            handler.Convert("15000", typeof(uint), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)86016, result);

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
        public void TestStringNumericConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringNumericConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
              .WithNumberStyle(NumberStyles.Any));


            object? result = null;
            handler.Convert($@"{byte.MinValue}", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)byte.MinValue, result);

            handler.Convert($@"{byte.MaxValue}", typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)byte.MaxValue, result);

            handler.Convert($@"{sbyte.MinValue}", typeof(sbyte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)sbyte.MinValue, result);

            handler.Convert($@"{sbyte.MaxValue}", typeof(sbyte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)sbyte.MaxValue, result);

            handler.Convert($@"{short.MinValue}", typeof(short), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)short.MinValue, result);

            handler.Convert($@"{short.MaxValue}", typeof(short), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)short.MaxValue, result);

            handler.Convert($@"{ushort.MinValue}", typeof(ushort), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ushort)ushort.MinValue, result);

            handler.Convert($@"{ushort.MaxValue}", typeof(ushort), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ushort)ushort.MaxValue, result);

            handler.Convert($@"{int.MinValue}", typeof(int), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)int.MinValue, result);

            handler.Convert($@"{int.MaxValue}", typeof(int), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)int.MaxValue, result);

            handler.Convert($@"{uint.MinValue}", typeof(uint), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)uint.MinValue, result);

            handler.Convert($@"{uint.MaxValue}", typeof(uint), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)uint.MaxValue, result);

            handler.Convert($@"{long.MinValue}", typeof(long), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((long)long.MinValue, result);

            handler.Convert($@"{long.MaxValue}", typeof(long), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((long)long.MaxValue, result);

            handler.Convert($@"{ulong.MinValue}", typeof(ulong), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ulong)ulong.MinValue, result);

            handler.Convert($@"{ulong.MaxValue}", typeof(ulong), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ulong)ulong.MaxValue, result);
        }

        [TestMethod]
        public void TestStringNumericHexAutodetectConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringNumericConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions())
                .WithHexDetection()
                .WithAddPrefix("HEXAAA!"));


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
            handler.AddConverter(new StringNumericConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions()));


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
            handler.AddConverter(new StringNumericConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions()));


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

        [TestMethod]
        public void TestFloatingConverter()
        {
            var outType = typeof(float);

            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringFloatingConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions()));


            object? result = null;
            handler.Convert($@"{float.MinValue}", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((float)float.MinValue, result);

            handler.Convert($@"{float.MaxValue}", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((float)float.MaxValue, result);

            outType = typeof(double);

            handler.Convert($@"{double.MinValue}", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((double)double.MinValue, result);

            handler.Convert($@"{double.MaxValue}", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((double)double.MaxValue, result);

            string nanString = float.NaN.ToString(CultureInfo.InvariantCulture);
            handler.Convert($@"{nanString}", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((double)double.NaN, result);

        }

        [TestMethod]
        public void TestDecimalConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringDecimalConverterOptions()
                .AddInputFilter(new StringTrimFilterOptions()));


            for (int i = 0; i < 1000000; i++)
            {
                var outType = typeof(decimal);


                object? result = null;
                handler.Convert($@"10.12345678", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)10.12345678, result);

                handler.Convert($@"{decimal.MinValue}", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)decimal.MinValue, result);


                handler.Convert($@"{decimal.MaxValue}", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)decimal.MaxValue, result);

                handler.Convert($@"{decimal.MinusOne}", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)decimal.MinusOne, result);

                handler.Convert($@"{decimal.One}", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)decimal.One, result);

                handler.Convert($@"{decimal.Zero}", outType, out result);

                Assert.IsNotNull(result);
                Assert.AreEqual((decimal)decimal.Zero, result);
            }
        }

        [TestMethod]
        public void TestNumericConverter()
        {

            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new NumericConverterOptions());


            object? result = null;
            handler.Convert((int)3, typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)3, result);
            Assert.AreEqual(result.GetType(), typeof(byte));

            handler.Convert((int)300, typeof(short), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)300, result);
            Assert.AreEqual(result.GetType(), typeof(short));

            Assert.ThrowsException<TypeConverterException>(() =>
            {
                handler.Convert((int)-100, typeof(ushort), out result);
            });

        }

        [TestMethod]
        public void TestDataUnit()
        {
            DataUnitTransformerOptions options = new DataUnitTransformerOptions();
            var transformer = options.Build();

            transformer.Transform(DataTransformOutput.From(1000000), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1, rtesult.Value);
        }

        [TestMethod]
        public void TestAutoScaleDataUnit()
        {
            AutoScaleDataUnitTransformerOptions options = new AutoScaleDataUnitTransformerOptions();
            var transformer = options.Build();

            transformer.Transform(DataTransformOutput.From(512.0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)512.0, rtesult.Value);

            transformer.Transform(DataTransformOutput.From(1024.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.0, rtesult.Value);

            transformer.Transform(DataTransformOutput.From(1536.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.5, rtesult.Value);

            transformer.Transform(DataTransformOutput.From((int)1_048_576), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.00, rtesult.Value);

            transformer.Transform(DataTransformOutput.From(1_610_612_736.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.50, rtesult.Value);

            transformer.Transform(DataTransformOutput.From(1_099_511_627_776.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.00, rtesult.Value);
        }

        [TestMethod]
        public void TestRangeTransform()
        {
            ScalingTransformerOptions options = new ScalingTransformerOptions()
                .WithInputMin(0).WithInputMax(65000)
                .WithOutputMin(-50).WithOutputMax(200);

            var transformer = options.Build();

            transformer.Transform(DataTransformOutput.From(512.0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)-48.030769230769231, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformOutput.From(0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)-50, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformOutput.From(65000), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)200, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformOutput.From((int)13000), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

        }

        [TestMethod]
        public void TestDeadbandTransform()
        {
            DataTransformerHandler handler = new DataTransformerHandler();
            handler.AddTransformer(new ScalingTransformerOptions()
                .WithInputMin(0).WithInputMax(100)
                .WithOutputMin(0).WithOutputMax(100));
            handler.AddTransformer(new DeadbandTransformerOptions()
                .WithDeadband(10.0));

            handler.Transform(DataTransformOutput.From((double)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            handler.Transform(DataTransformOutput.From((double)6), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            handler.Transform(DataTransformOutput.From((int)11), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)11, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

        }

        [TestMethod]
        public void TestDeadbandIntegralTransform()
        {
            DataTransformerHandler handler = new DataTransformerHandler();
            handler.AddTransformer(new DeadbandTransformerOptions()
                .WithDeadband(10.0));

            handler.Transform(DataTransformOutput.From((int)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)0, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

            handler.Transform(DataTransformOutput.From((int)6), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)0, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

            handler.Transform(DataTransformOutput.From((int)11), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)11, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

        }
    }
}
