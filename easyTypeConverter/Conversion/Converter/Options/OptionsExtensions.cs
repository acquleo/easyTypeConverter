using easyTypeConverter.Conversion.Filters.Options;
using System.Globalization;

namespace easyTypeConverter.Conversion.Converter.Options
{

    public static class IStringTextInputOptionsExtensions
    {

        /// <summary>
        /// Imposta la proprietà <see cref="IStringTextInputOptions.Comparison"/> dell'istanza specificata di <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringTextInputOptions"/>.
        /// </typeparam>
        /// <param name="obj">
        /// L'oggetto opzioni a cui verrà assegnata la proprietà <c>Comparison</c>.
        /// </param>
        /// <param name="comparison">
        /// Il valore <see cref="StringComparison"/> da assegnare alla proprietà <c>Comparison</c>.
        /// </param>
        /// <returns>
        /// L'oggetto opzioni modificato, permettendo la concatenazione fluente.
        /// </returns>
        public static T WithComparison<T>(this T obj, StringComparison comparison)
                where T : IStringTextInputOptions
        {
            obj.Comparison = comparison;
            return obj;
        }
    }


    public static class ITypeConverterOptionsExtensions
    {
        /// <summary>
        /// Aggiunge un filtro di input all'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="ITypeConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui aggiungere il filtro di input.</param>
        /// <param name="options">Le opzioni del filtro da aggiungere.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T AddInputFilter<T>(this T obj, IFilterOptions options)
                where T : ITypeConverterOptions
        {
            obj.InputFilters.Add(options);
            return obj;
        }

        /// <summary>
        /// Aggiunge un filtro di output all'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="ITypeConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui aggiungere il filtro di output.</param>
        /// <param name="options">Le opzioni del filtro da aggiungere.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T AddOutputFilter<T>(this T obj, IFilterOptions options)
                where T : ITypeConverterOptions
        {
            obj.OutputFilters.Add(options);
            return obj;
        }
    }

    public static class IStringNumericConverterBaseOptionsExtensions
    {
        /// <summary>
        /// Imposta la proprietà <see cref="IStringIntegralConverterBaseOptions.NumberStyle"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringIntegralConverterBaseOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare lo stile numerico.</param>
        /// <param name="style">Lo stile numerico da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithNumberStyle<T>(this T obj, NumberStyles style)
                where T : IStringIntegralConverterBaseOptions
        {
            obj.NumberStyle = style;
            return obj;
        }

        /// <summary>
        /// Imposta la proprietà <see cref="IStringIntegralConverterBaseOptions.Culture"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringIntegralConverterBaseOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare la cultura.</param>
        /// <param name="culture">La cultura da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithCulture<T>(this T obj, string culture)
               where T : IStringIntegralConverterBaseOptions
        {
            obj.Culture = culture;
            return obj;
        }
        /// <summary>
        /// Abilita la rilevazione esadecimale sull'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringIntegralConverterBaseOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni su cui abilitare la rilevazione esadecimale.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithHexDetection<T>(this T obj)
                where T : IStringIntegralConverterBaseOptions
        {
            obj.HexDetection = true;
            return obj;
        }

        /// <summary>
        /// Aggiunge un prefisso esadecimale all'oggetto opzioni specificato se non già presente.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringIntegralConverterBaseOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui aggiungere il prefisso.</param>
        /// <param name="prefix">Il prefisso esadecimale da aggiungere.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithAddPrefix<T>(this T obj, string prefix)
                where T : IStringIntegralConverterBaseOptions
        {
            if (!obj.HexPrefixList.Contains(prefix))
                obj.HexPrefixList.Add(prefix);
            return obj;
        }
    }
    public static class IFloatingStringConverterOptionsExtensions
    {
        /// <summary>
        /// Imposta la proprietà <see cref="IFloatingStringConverterOptions.Format"/> dell'oggetto opzioni specificato. Ad esempio: "G", "F2", "0.00", ecc. 
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IFloatingStringConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare il formato.</param>
        /// <param name="format">Il formato da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithFormat<T>(this T obj, string format)
                where T : IFloatingStringConverterOptions
        {
            obj.Format = format;
            return obj;
        }

        /// <summary>
        /// Imposta la proprietà <see cref="IFloatingStringConverterOptions.Culture"/> dell'oggetto opzioni specificato. Ad esempio: CultureInfo.InvariantCulture, new CultureInfo("en-US"), ecc.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IFloatingStringConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare la cultura.</param>
        /// <param name="culture">La cultura da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithCulture<T>(this T obj, string culture)
                where T : IFloatingStringConverterOptions
        {
            obj.Culture = culture;
            return obj;
        }

    }
    public static class IStringTimeSpanConverterOptionsExtensions
    {
        /// <summary>
        /// Imposta la proprietà <see cref="IStringTimeSpanConverterOptions.Culture"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringTimeSpanConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare la cultura.</param>
        /// <param name="culture">La cultura da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithCulture<T>(this T obj, string culture)
            where T : IStringTimeSpanConverterOptions
        {
            obj.Culture = culture;
            return obj;
        }
        
        /// <summary>
        /// Imposta la proprietà <see cref="IStringTimeSpanConverterOptions.Formats"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="IStringTimeSpanConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare i formati.</param>
        /// <param name="formats">I formati da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithFormats<T>(this T obj, params string[] formats)
            where T : IStringTimeSpanConverterOptions
        {
            obj.Formats = formats;
            return obj;
        }
    }
    public static class ITimeSpanStringConverterOptionsExtensions
    {
        /// <summary>
        /// Imposta la proprietà <see cref="ITimeSpanStringConverterOptions.Format"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="ITimeSpanStringConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare il formato.</param>
        /// <param name="format">Il formato da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithFormat<T>(this T obj, string format)
            where T : ITimeSpanStringConverterOptions
        {
            obj.Format = format;
            return obj;
        }

        /// <summary>
        /// Imposta la proprietà <see cref="ITimeSpanStringConverterOptions.Culture"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="ITimeSpanStringConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare la cultura.</param>
        /// <param name="culture">La cultura da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithCulture<T>(this T obj, string culture)
            where T : ITimeSpanStringConverterOptions
        {
            obj.Culture = culture;
            return obj;
        }
    }
    public static class ITimeSpanNumberConverterOptionsExtensions
    {
        /// <summary>
        /// Imposta la proprietà <see cref="ITimeSpanNumberConverterOptions.Unit"/> dell'oggetto opzioni specificato.
        /// </summary>
        /// <typeparam name="T">Il tipo dell'oggetto opzioni, che deve implementare <see cref="ITimeSpanNumberConverterOptions"/>.</typeparam>
        /// <param name="obj">L'oggetto opzioni a cui assegnare l'unità di misura.</param>
        /// <param name="unit">L'unità di misura da assegnare.</param>
        /// <returns>L'oggetto opzioni modificato, permettendo la concatenazione fluente.</returns>
        public static T WithUnit<T>(this T obj, TimeSpanNumberUnit unit)
            where T : ITimeSpanNumberConverterOptions
        {
            obj.Unit = unit;
            return obj;
        }

    }
    public static class INumberTimeSpanConverterOptionsExtensions
    {
        public static T WithUnit<T>(this T obj, TimeSpanNumberUnit unit)
            where T : INumberTimeSpanConverterOptions
        {
            obj.Unit = unit;
            return obj;
        }
    }
    public static class INumberDateTimeConverterOptionsExtensions
    {
        public static T WithUnit<T>(this T obj, NumberDateTimeUnit unit)
            where T : INumberDateTimeConverterOptions
        {
            obj.Unit = unit;
            return obj;
        }
        public static T WithEpoch<T>(this T obj, DateTime epoch)
            where T : INumberDateTimeConverterOptions
        {
            obj.Epoch = epoch;
            return obj;
        }
        public static T WithKind<T>(this T obj, DateTimeKind kind)
            where T : INumberDateTimeConverterOptions
        {
            obj.Kind = kind;
            return obj;
        }
    }
    public static class StringDateTimeConverterOptionsExtensions
    {
        public static T WithFormats<T>(this T obj, params string[] formats)
            where T : IStringDateTimeConverterOptions
        {
            obj.Formats = formats;
            return obj;
        }
        public static T WithCulture<T>(this T obj, string culture)
            where T : IStringDateTimeConverterOptions
        {
            obj.Culture = culture;
            return obj;
        }
        public static T WithDateTimeStyles<T>(this T obj, DateTimeStyles styles)
            where T : IStringDateTimeConverterOptions
        {
            obj.DateTimeStyles = styles;
            return obj;
        }
    }
    public static class IDateTimeNumberConverterOptionsExtensions
    {
        public static T WithUnit<T>(this T obj, DateTimeNumberUnit unit)
            where T : IDateTimeNumberConverterOptions
        {
            obj.Unit = unit;
            return obj;
        }
        public static T WithEpoch<T>(this T obj, DateTime epoch)
            where T : IDateTimeNumberConverterOptions
        {
            obj.Epoch = epoch;
            return obj;
        }
    }

    public static class FloatingIntegralConverterOptionsExtensions
    {

    }
}
