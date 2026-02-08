// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Conversion;
using ConsoleApp1.Conversion.Converter;
using ConsoleApp1.Conversion.Converter.Options;
using ConsoleApp1.Conversion.Filters;
using ConsoleApp1.Conversion.Filters.Options;
using System.Globalization;

Console.WriteLine("Hello, World!");


TypeConverterHandler handler = new TypeConverterHandler();

handler.AddConverter(new StringBooleanConverter(new StringBooleanConverterOptions()
    .AddInputFilter(new StringNumericBooleanFilterOptions())));
handler.AddConverter(new BooleanStringConverter(new  BooleanStringConverterOptions()));
handler.AddConverter(new Int32StringConverter());

var opt2 = new StringBooleanConverterOptions()
    .AddInputFilter(new StringTrimFilterOptions())
    .AddInputFilter(new StringRegexMatchReplaceFilterOptions()
        .WithReplace("True",".* ACTIVATED")
        .WithReplace("False", ".* DEACTIVATED"))
    .AddInputFilter(new StringReplaceFilterOptions()
        .WithComparer(StringComparison.InvariantCultureIgnoreCase)
        .WithReplace("True", "on", "positive")
        .WithReplace("False", "off", "negative"))
    .AddInputFilter(new StringNumericBooleanFilterOptions());

handler.AddConverter(new StringBooleanConverter(opt2));

opt2.Build().Convert("poloo ACTIVATED", out var res);
opt2.Build().Convert("0", out var resa);
opt2.Build().Convert("12", out var resb);

handler.AddConverter(new StringByteConverter(
    new StringByteConverterOptions()));

var opt = new StringByteConverterOptions();
opt.NumberStyle = NumberStyles.HexNumber;
opt.InputFilters.Add(new StringRemovePrefixFilterOptions()
{
    Prefix = "0x"
});
handler.AddConverter(opt.Build());

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
