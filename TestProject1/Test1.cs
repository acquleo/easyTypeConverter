using easyTypeConverter.Common;
using easyTypeConverter.Conversion;
using easyTypeConverter.Conversion.Converter;
using easyTypeConverter.Conversion.Converter.Options;
using easyTypeConverter.Conversion.Exceptions;
using easyTypeConverter.Conversion.Filters.Options;
using easyTypeConverter.Evaluating;
using easyTypeConverter.Evaluating.Evaluators.Options;
using easyTypeConverter.Formatting.Formatter;
using easyTypeConverter.Formatting.Formatter.Options;
using easyTypeConverter.Serialization;
using easyTypeConverter.Transformation;
using easyTypeConverter.Transformation.Transformer;
using easyTypeConverter.Transformation.Transformer.Options;
using easyTypeConverter.Triggering;
using easyTypeConverter.Triggering.Evaluator.Options;
using Moq;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text.Json;

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
        public void TestExpressionGreather()
        {
            NcalcExpressionEvaluatorOptions options = new NcalcExpressionEvaluatorOptions()
                .WithExpression("val(\"A\") > val(\"B\")");


            var mockHandler = new Mock<IEvaluatorContext>();
            
            
            var evaluator = options.Build(mockHandler.Object);

            mockHandler.Reset();

            mockHandler.Setup(h => h.Evaluate(It.IsAny<ParamType>(), It.IsAny<string>(), It.IsAny<object[]>()))
           .Returns((ParamType paramType, string name, object[] args) =>
           {
               if (paramType == ParamType.Function && name == "val" && args.Length == 1 && args[0].ToString() == "A") return 10;
               if (paramType == ParamType.Function && name == "val" && args.Length == 1 && args[0].ToString() == "B") return 2;
               return null; // default

           });


            evaluator.Analyze();


            var result = evaluator.Evaluate();

            mockHandler.Verify(m => m.Analyze(ParamType.Function, "val", It.Is<object[]>(arr => arr.Length == 1 && arr[0].Equals("A"))), Times.Once());
            mockHandler.Verify(m => m.Analyze(ParamType.Function, "val", It.Is<object[]>(arr => arr.Length == 1 && arr[0].Equals("B"))), Times.Once());

            mockHandler.Verify(m => m.Evaluate(ParamType.Function, "val", It.Is<object[]>(arr => arr.Length == 1 && arr[0].Equals("A"))), Times.Once());
            mockHandler.Verify(m => m.Evaluate(ParamType.Function, "val", It.Is<object[]>(arr => arr.Length == 1 && arr[0].Equals("B"))), Times.Once());

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        [Timeout(20000)]
        public void TestExpressionPerf()
        {
            NcalcExpressionEvaluatorOptions options = new NcalcExpressionEvaluatorOptions()
                .WithExpression("val(\"A\") > val(\"B\")");


            var evaluator = options.Build(new EvaluatorContext());

            for (int i = 0; i < 10000000; i++)
            {
                

                evaluator.Analyze();


                var result = evaluator.Evaluate();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
        }
        [TestMethod]
        public void TestExpressionEqual()
        {
            NcalcExpressionEvaluatorOptions options = new NcalcExpressionEvaluatorOptions()
                .WithExpression("val == null");


            var mockHandler = new Mock<IEvaluatorContext>();
            mockHandler.Setup(h => h.Evaluate(It.IsAny<ParamType>(), It.IsAny<string>(), It.IsAny<object[]>()))
               .Returns((ParamType paramType, string name, object[] args) =>
               {
                   if (paramType == ParamType.Param && name == "val" && args.Length == 0) return null;
                   if (paramType == ParamType.Param && name == "null" && args.Length == 0) return null;
                   return null; // default
               });

            var evaluator = options.Build(mockHandler.Object);

            evaluator.Analyze();

            mockHandler.Verify(m => m.Analyze(ParamType.Param, "val", It.IsAny<object[]>()), Times.Once());
            mockHandler.Verify(m => m.Analyze(ParamType.Param, "null", It.IsAny<object[]>()), Times.Once());
            mockHandler.Reset();

            var result = evaluator.Evaluate();

            mockHandler.Verify(m => m.Evaluate(ParamType.Param, "val", It.IsAny<object[]>()), Times.Once());
            mockHandler.Verify(m => m.Evaluate(ParamType.Param, "null", It.IsAny<object[]>()), Times.Once());

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

            [TestMethod]
        public void TestDataEvaluator()
        {
            SetStatusActionOptions action1 = new SetStatusActionOptions();
            action1.StatusToBeSet = "ACTIVE";

            SetStatusActionOptions action2 = new SetStatusActionOptions();
            action2.StatusToBeSet = "INACTIVE";

            EqualityTriggerOptions options = new EqualityTriggerOptions()
                .WithValueToCompare("pippo")
                .WithAction(action1);

            var evaluator = options.Build(new ActionHandler());
            evaluator.Evaluate(new TriggerInputContext { Value = "pippo" });

            NcalcExpressionTriggerOptions options2 = new NcalcExpressionTriggerOptions()
                .WithExpression("val() == \"pippo\"")
                .WithAction(action1);

            for (int i = 0; i < 10000; i++)
            {
                evaluator = options2.Build(new ActionHandler());
            
            
                evaluator.Evaluate(new TriggerInputContext { Value = "pippo" });
            }

            NcalcExpressionTriggerOptions options3 = new NcalcExpressionTriggerOptions()
                .WithExpression("sts() == null()")
                .WithAction(action1);

            evaluator = options3.Build(new ActionHandler());

            evaluator.Evaluate(new TriggerInputContext { Value = "pippo" });

            DataEvaluatorSerializer serializer = new DataEvaluatorSerializer();

            serializer.RegisterEvaluatorAction<SetStatusActionOptions>("set_status");

            string json = serializer.SerializeEvaluator(options);

            var test = serializer.DeserializeEvaluator(json);

            test.Build(new ActionHandler()).Evaluate(new TriggerInputContext { Value = "pippo" });
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
            handler.Convert("0", DataTypes.FromType(typeof(Boolean)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);

            handler.Convert("1", DataTypes.FromType(typeof(Boolean)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            handler.Convert(" 1", DataTypes.FromType(typeof(Boolean)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            handler.Convert("12", DataTypes.FromType(typeof(Boolean)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("FF", DataTypes.FromType(typeof(Boolean)), out result);
            });


            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("0x00", DataTypes.FromType(typeof(Boolean)), out result);
            });

            handler.Convert("-4", DataTypes.FromType(typeof(Boolean)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("pippo", DataTypes.FromType(typeof(Boolean)), out result);
            });

            handler.Convert("True", DataTypes.FromType(typeof(Boolean)), out result);
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
            handler.Convert("0", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            handler.Convert("1", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)1, result);

            handler.Convert("12", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("15000", DataTypes.FromType(typeof(UInt32)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)86016, result);

            handler.Convert("0xFF", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)255, result);

            handler.Convert("0x00", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("-4", DataTypes.FromType(typeof(Byte)), out result);
            });


            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("pippo", DataTypes.FromType(typeof(Byte)), out result);
            });

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("True", DataTypes.FromType(typeof(Byte)), out result);
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
            handler.Convert($@"{byte.MinValue}", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)byte.MinValue, result);

            handler.Convert($@"{byte.MaxValue}", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)byte.MaxValue, result);

            handler.Convert($@"{sbyte.MinValue}", DataTypes.FromType(typeof(SByte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)sbyte.MinValue, result);

            handler.Convert($@"{sbyte.MaxValue}", DataTypes.FromType(typeof(SByte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)sbyte.MaxValue, result);

            handler.Convert($@"{short.MinValue}", DataTypes.FromType(typeof(Int16)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)short.MinValue, result);

            handler.Convert($@"{short.MaxValue}", DataTypes.FromType(typeof(Int16)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)short.MaxValue, result);

            handler.Convert($@"{ushort.MinValue}", DataTypes.FromType(typeof(UInt16)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ushort)ushort.MinValue, result);

            handler.Convert($@"{ushort.MaxValue}", DataTypes.FromType(typeof(UInt16)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((ushort)ushort.MaxValue, result);

            handler.Convert($@"{int.MinValue}", DataTypes.FromType(typeof(Int32)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)int.MinValue, result);

            handler.Convert($@"{int.MaxValue}", typeof(int), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)int.MaxValue, result);

            handler.Convert($@"{uint.MinValue}", DataTypes.FromType(typeof(UInt32)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)uint.MinValue, result);

            handler.Convert($@"{uint.MaxValue}", DataTypes.FromType(typeof(UInt32)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((uint)uint.MaxValue, result);

            handler.Convert($@"{long.MinValue}", DataTypes.FromType(typeof(Int64)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((long)long.MinValue, result);

            handler.Convert($@"{long.MaxValue}", DataTypes.FromType(typeof(Int64)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((long)long.MaxValue, result);

            handler.Convert($@"{ulong.MinValue}", DataTypes.FromType(typeof(UInt64)), out result);

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
            handler.Convert("0", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)0, result);

            handler.Convert("1", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)1, result);

            handler.Convert("12", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)12, result);

            handler.Convert("0x12", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("&h12", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);

            handler.Convert("0X12", DataTypes.FromType(typeof(Byte)), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)18, result);


            handler.Convert("HEXAAA!FA", DataTypes.FromType(typeof(Byte)), out result);

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

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("0xFF", typeof(byte), out result);
            });


            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("0x00", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("-4", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("pippo", typeof(byte), out result);
            });

            Assert.ThrowsException<TypeConvertFailedException>(() =>
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

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("0xFF", outType, out result);
            });


            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("0x00", outType, out result);
            });

            handler.Convert("-4", outType, out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((sbyte)-4, result);

            Assert.ThrowsException<TypeConvertFailedException>(() =>
            {
                handler.Convert("pippo", outType, out result);
            });

            Assert.ThrowsException<TypeConvertFailedException>(() =>
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

        [TestMethod]
        public void TestNumericConverter()
        {

            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new IntegralConverterOptions());


            object? result = null;
            handler.Convert((int)3, typeof(byte), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((byte)3, result);
            Assert.AreEqual(result.GetType(), typeof(byte));

            handler.Convert((int)300, typeof(short), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((short)300, result);
            Assert.AreEqual(result.GetType(), typeof(short));

            Assert.ThrowsException<TypeConvertException>(() =>
            {
                handler.Convert((int)-100, typeof(ushort), out result);
            });

        }

        [TestMethod]
        public void TestDataUnit()
        {
            DataUnitTransformerOptions options = new DataUnitTransformerOptions();
            var transformer = options.Build();

            transformer.Transform(DataTransformContext.From(1000000), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1, rtesult.Value);
        }

        [TestMethod]
        public void TestAutoScaleDataUnit()
        {
            AutoScaleDataUnitTransformerOptions options = new AutoScaleDataUnitTransformerOptions();
            var transformer = options.Build();

            transformer.Transform(DataTransformContext.From(512.0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)512.0, rtesult.Value);

            transformer.Transform(DataTransformContext.From(1024.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.0, rtesult.Value);

            transformer.Transform(DataTransformContext.From(1536.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.5, rtesult.Value);

            transformer.Transform(DataTransformContext.From((int)1_048_576), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.00, rtesult.Value);

            transformer.Transform(DataTransformContext.From(1_610_612_736.0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)1.50, rtesult.Value);

            transformer.Transform(DataTransformContext.From(1_099_511_627_776.0), out rtesult);

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

            transformer.Transform(DataTransformContext.From(512.0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)-48.030769230769231, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformContext.From(0), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)-50, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformContext.From(65000), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)200, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            transformer.Transform(DataTransformContext.From((int)13000), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

        }

        [TestMethod]
        public void TestDeadbandTransform()
        {
            DataTransformerHandlerOptions options = new DataTransformerHandlerOptions();

            options.AddTransformer(new ScalingTransformerOptions()
                .WithInputMin(0).WithInputMax(100)
                .WithOutputMin(0).WithOutputMax(100));
            options.AddTransformer(new DeadbandTransformerOptions()
                .WithDeadband(10.0));

            DataTransformerHandler handler = options.Build();

            handler.Transform(DataTransformContext.From((double)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((double)6), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((double)0, rtesult.Value);
            Assert.AreEqual(typeof(double), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)11), out rtesult);

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

            handler.Transform(DataTransformContext.From((int)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)0, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)6), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)0, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)11), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((int)11, rtesult.Value);
            Assert.AreEqual(typeof(int), rtesult.Value.GetType());

        }

        [TestMethod]
        public void TestBitMaksTransform()
        {
            DataTransformerHandler handler = new DataTransformerHandler();
            handler.AddTransformer(new BitMaskTransformerOptions()
                .WithMask(0x02));

            handler.Transform(DataTransformContext.From((int)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((bool)false, rtesult.Value);
            Assert.AreEqual(typeof(bool), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)7), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((bool)true, rtesult.Value);
            Assert.AreEqual(typeof(bool), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)2), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((bool)true, rtesult.Value);

            Assert.AreEqual(typeof(bool), rtesult.Value.GetType());

        }

        [TestMethod]
        public void TestBitMaksMultibitTransform()
        {
            DataTransformerHandler handler = new DataTransformerHandler();
            handler.AddTransformer(new BitMaskTransformerOptions()
                .WithMask(0x06).WithNormalize());

            handler.Transform(DataTransformContext.From((int)0), out var rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((ulong)0, rtesult.Value);
            Assert.AreEqual(typeof(ulong), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)2), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((ulong)1, rtesult.Value);
            Assert.AreEqual(typeof(ulong), rtesult.Value.GetType());

            handler.Transform(DataTransformContext.From((int)7), out rtesult);

            Assert.IsNotNull(rtesult);
            Assert.AreEqual((ulong)3, rtesult.Value);
            Assert.AreEqual(typeof(ulong), rtesult.Value.GetType());
        }

        [TestMethod]
        public void TestTemplateFormatter()
        {
            TemplateFormatterOptions formatterOptions = new TemplateFormatterOptions()
                .WithTemplate("PIPPO &ob;{value}&cb;");
            var formatter = formatterOptions.Build();

            formatter.Format(FormatterContext.From((double)42.3), out var result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"PIPPO {42.3}", result);

            formatterOptions.WithTemplate("{value:F2}");

            formatter.Format(FormatterContext.From((double)42.3876), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"42.39", result);

            formatterOptions.WithTemplate("&ob;&ob;text&cb;&cb; &ob;{value:F2}&cb;");

            formatter.Format(FormatterContext.From((double)42.3876), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"{{text}} {42.39}", result);

            formatterOptions.WithTemplate("{value:F1} {unit}");

            formatter.Format(FormatterContext.From((double)42.3876, Units.Temperature.Celsius), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"42.4 °C", result);

            formatterOptions.WithTemplate("{value:F2} [{status}]");

            formatter.Format(FormatterContext.From((double)42.3876, Units.Temperature.Celsius, "FAULT"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"42.39 [FAULT]", result);

            formatterOptions.WithTemplate("{value:o} DIA_STATUS_{status}");

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0, DateTimeKind.Utc), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"1920-10-12T10:02:45.1200000Z DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}");

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 10:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Local);

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0, DateTimeKind.Utc), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 12:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Utc);

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0, DateTimeKind.Local), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 08:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Local);

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0, DateTimeKind.Local), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 10:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Utc);

            formatter.Format(FormatterContext.From((DateTime)new DateTime(1920, 10, 12, 10, 2, 45, 120, 0, DateTimeKind.Utc), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 10:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Utc);

            formatter.Format(FormatterContext.From((DateTimeOffset)new DateTimeOffset(1920, 10, 12, 10, 2, 45, 120, 0, TimeSpan.FromHours(0)), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 10:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Local);

            formatter.Format(FormatterContext.From((DateTimeOffset)new DateTimeOffset(1920, 10, 12, 10, 2, 45, 120, 0, TimeSpan.FromHours(0)), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 12:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Utc);

            formatter.Format(FormatterContext.From((DateTimeOffset)new DateTimeOffset(1920, 10, 12, 10, 2, 45, 120, 0, TimeSpan.FromHours(2)), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 08:02:45.120 DIA_STATUS_GOOD", result);

            formatterOptions.WithTemplate("{value:dd/MM/yyyy HH:mm:ss.fff} DIA_STATUS_{status}").WithDateTimeConvert(DateTimeKind.Local);

            formatter.Format(FormatterContext.From((DateTimeOffset)new DateTimeOffset(1920, 10, 12, 10, 2, 45, 120, 0, TimeSpan.FromHours(2)), status: "GOOD"), out result);

            Assert.IsNotNull(result);
            Assert.AreEqual((string)"12/10/1920 10:02:45.120 DIA_STATUS_GOOD", result);

        }


        [TestMethod]
        public void TestJson()
        {
            DataTransformerHandlerOptions options = new DataTransformerHandlerOptions();

            options.AddTransformer(new TypeConverterTransformerOptions()
                .WithConverter(new FloatingIntegralConverterOptions())
                .WithTargetType(DataTypes.Int32));
            options.AddTransformer(new ScalingTransformerOptions()
                .WithInputMin(0).WithInputMax(100)
                .WithOutputMin(0).WithOutputMax(10000));
            options.AddTransformer(new DeadbandTransformerOptions()
                .WithDeadband(10.0));
            options.AddTransformer(new AutoScaleDataUnitTransformerOptions()
                .WithDecimal()
                .WithSourceDataUnit(DataUnit.Byte)
                .WithScaleThreshold(1));

            DataTransformSerializer serializer = new DataTransformSerializer();

            string json = serializer.Serialize(options);

            DataTransformerHandlerOptions? deserializedOptions = serializer.Deserialize(json);

            if (deserializedOptions == null)
                throw new InvalidOperationException();

            var handler = deserializedOptions.Build();

            handler.Transform(DataTransformContext.From((double)50), out var result);

            Assert.IsNotNull(result);

            var testsingle = new AutoScaleDataUnitTransformerOptions()
                .WithDecimal()
                .WithSourceDataUnit(DataUnit.Bit)
                .WithScaleThreshold(100.0);

            
            string json_single = serializer.SerializeTransformer(testsingle);

            DataTransformerOptions? deserializedOptions_single = serializer.DeserializeTransform(json_single);

            Assert.IsNotNull(deserializedOptions_single);

            var manual_json = $@"{{
              ""transformers"": [
                {{
                  ""$type"": ""scaling"",
                  ""inputMin"": 0,
                  ""inputMax"": 100,
                  ""outputMin"": 0,
                  ""outputMax"": 10000
                }},
                {{
                  ""$type"": ""deadband"",
                  ""deadband"": 10
                }},
                {{
                  ""$type"": ""autoscale_dataunit"",
                  ""sourceUnit"": ""Byte"",
                  ""useBinary"": false,
                  ""scaleThreshold"": 1
                }}
              ]
            }}";

            DataConverterSerializer dataConverterSerializer = new DataConverterSerializer();

            DataTransformerHandlerOptions? manual_obj = serializer.Deserialize(manual_json);
            var resulto = manual_obj.Build().Transform(DataTransformContext.From((double)50), out var manual_result);


            BooleanStringConverterOptions optionsB = new BooleanStringConverterOptions();
            var booh = dataConverterSerializer.SerializeConverter(optionsB);
        }

        [TestMethod]
        public void TestNumberDateTimeConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new NumberDateTimeConverterOptions()
                .WithUnit(NumberDateTimeUnit.Seconds)
                .WithEpoch(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .WithKind(DateTimeType.Utc));
            object? result = null;
            handler.Convert(0, DataTypes.DateTime, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), result);
            handler.Convert(3600, DataTypes.DateTime, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(1970, 1, 1, 1, 0, 0, DateTimeKind.Utc), result);
        }

        [TestMethod]
        public void TestDateTimeNumberConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new DateTimeNumberConverterOptions()
                .WithUnit(DateTimeNumberUnit.Seconds)
                .WithEpoch(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
            object? result = null;
            var dt = new DateTime(1970, 1, 1, 1, 0, 0, DateTimeKind.Utc);
            handler.Convert(dt, DataTypes.Double, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(3600.0, result);
        }

        [TestMethod]
        public void TestStringDateTimeConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringDateTimeConverterOptions()
                .WithFormats("yyyy-MM-dd HH:mm:ss")
                .WithCulture("en-US"));
            object? result = null;
            handler.Convert("2024-06-01 12:34:56", DataTypes.DateTime, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(2024, 6, 1, 12, 34, 56), result);
        }

        [TestMethod]
        public void TestTimeSpanNumberConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new TimeSpanNumberConverterOptions()
                .WithUnit(TimeSpanNumberUnit.Seconds));
            object? result = null;
            handler.Convert(TimeSpan.FromMinutes(2), DataTypes.Double, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(120.0, result);
        }

        [TestMethod]
        public void TestNumberTimeSpanConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new NumberTimeSpanConverterOptions()
                .WithUnit(TimeSpanNumberUnit.Minutes));
            object? result = null;
            handler.Convert(2, DataTypes.TimeSpan, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(TimeSpan.FromMinutes(2), result);
        }

        [TestMethod]
        public void TestStringTimeSpanConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new StringTimeSpanConverterOptions()
                .WithFormats("c")
                .WithCulture("en-US"));
            object? result = null;
            handler.Convert("01:02:03", DataTypes.TimeSpan, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(new TimeSpan(1, 2, 3), result);
        }

        [TestMethod]
        public void TestTimeSpanStringConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new TimeSpanStringConverterOptions()
                .WithFormat("c")
                .WithCulture("en-US"));
            object? result = null;
            handler.Convert(new TimeSpan(1, 2, 3), DataTypes.String, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual("01:02:03", result);
        }

        [TestMethod]
        public void TestFloatingDecimalConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new FloatingDecimalConverterOptions());
            object? result = null;
            handler.Convert(1.5, DataTypes.Decimal, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual((decimal)1.5, result);
        }

        [TestMethod]
        public void TestDecimalIntegralConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new DecimalIntegralConverterOptions());
            object? result = null;
            handler.Convert((decimal)42.0, DataTypes.Int32, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void TestDecimalStringConverter()
        {
            TypeConverterHandler handler = new TypeConverterHandler();
            handler.AddConverter(new DecimalStringConverterOptions()
                .WithFormat("F2")
                .WithCulture("en-US"));
            object? result = null;
            handler.Convert((decimal)42.1234, DataTypes.String, out result);
            Assert.IsNotNull(result);
            Assert.AreEqual("42.12", result);
        }
    }
}
