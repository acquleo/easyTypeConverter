// See https://aka.ms/new-console-template for more information
using easyTypeConverter;
using easyTypeConverter.Converter;
using easyTypeConverter.Converter.Options;
using easyTypeConverter.Filters;
using easyTypeConverter.Filters.Options;
using System.Globalization;

Console.WriteLine("Hello, World!");


TypeConverterHandler handler = new TypeConverterHandler();

handler.AddConverter(new StringBooleanConverterOptions()
    .AddInputFilter(new StringNumericBooleanFilterOptions()));

handler.AddConverter(new BooleanStringConverterOptions());

handler.AddConverter(new Int32StringConverterOptions());

var opt2 = new StringBooleanConverterOptions()
    .AddInputFilter(new StringTrimFilterOptions())
    .AddInputFilter(new StringRegexMatchReplaceFilterOptions()
        .WithReplace("True",".* ACTIVATED")
        .WithReplace("False", ".* DEACTIVATED")
        .WithExitOnMatch())
    .AddInputFilter(new StringReplaceFilterOptions()
        .WithComparer(StringComparison.InvariantCultureIgnoreCase)
        .WithReplace("True", "on", "positive")
        .WithReplace("False", "off", "negative")
        .WithExitOnMatch())
    .AddInputFilter(new StringNumericBooleanFilterOptions()
    .WithExitOnMatch());


handler.AddConverter(opt2);

opt2.Build().Convert("poloo ACTIVATED", out var res);
opt2.Build().Convert("0", out var resa);
opt2.Build().Convert("12", out var resb);

handler.AddConverter(new StringNumericConverterOptions());

var opt = new StringNumericConverterOptions();
opt.NumberStyle = NumberStyles.HexNumber;
opt.InputFilters.Add(new StringRemovePrefixFilterOptions()
{
    Prefix = "0x"
});
handler.AddConverter(opt);

object? result = null;
handler.Convert("0", typeof(Boolean), out result);
Console.WriteLine(result);

handler.Convert("1", typeof(Boolean), out result);
Console.WriteLine(result);

handler.Convert("True", typeof(Boolean), out result);
Console.WriteLine(result);

handler.Convert(true, typeof(string), out result);
Console.WriteLine(result);

handler.Convert(12, typeof(string), out result);
Console.WriteLine(result);

handler.Convert("POSITIVE", typeof(bool), out result);
Console.WriteLine(result);

handler.Convert("12", typeof(byte), out result);
Console.WriteLine(result);

handler.Convert("0xF", typeof(byte), out result);
Console.WriteLine(result);

TypeConverterHandler handlertest = new TypeConverterHandler();
handlertest.AddConverter(new StringDecimalConverterOptions()
    .AddInputFilter(new StringTrimFilterOptions()));


long hit = 0;
for (int i = 0; i < 1000000; i++)
{
    var outType = typeof(decimal);

    if (handlertest.Convert($@"10.12345678", outType, out result)) hit++;


    if (handlertest.Convert($@"{decimal.MinValue}", outType, out result)) hit++;



    if (handlertest.Convert($@"{decimal.MaxValue}", outType, out result)) hit++;



    if (handlertest.Convert($@"{decimal.MinusOne}", outType, out result)) hit++;


    if (handlertest.Convert($@"{decimal.One}", outType, out result)) hit++;



    if (handlertest.Convert($@"{decimal.Zero}", outType, out result)) hit++;

    
}

Console.WriteLine(hit);