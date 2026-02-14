    using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    /// <summary>
    /// Normalizes numeric values to a percentage range [0, 1] based on specified minimum and maximum bounds.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The percentage normalizer transformer converts values from an arbitrary range to a normalized
    /// range between 0 and 1, where 0 represents the minimum value and 1 represents the maximum value.
    /// </para>
    /// <para>
    /// The transformation uses the formula:
    /// <c>normalized = (value - Min) / (Max - Min)</c>
    /// </para>
    /// <para>
    /// This is commonly used for:
    /// <list type="bullet">
    /// <item><description>Converting values to percentages (multiply by 100 for 0-100%)</description></item>
    /// <item><description>Normalizing data for machine learning or statistical analysis</description></item>
    /// <item><description>Creating progress indicators from absolute values</description></item>
    /// <item><description>Standardizing values to a common scale for comparison</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Normalize temperature range 0-100°C to 0-1
    /// var options = new PercentageNormalizerTransformerOptions 
    /// { 
    ///     Min = 0, 
    ///     Max = 100
    /// };
    /// var transformer = new PercentageNormalizerTransformer(options);
    /// // Input: 50 → Output: 0.5
    /// // Input: 75 → Output: 0.75
    /// </code>
    /// </example>
    public class PercentageNormalizerTransformer : DataTransformer
    {
        readonly PercentageNormalizerTransformerOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentageNormalizerTransformer"/> class with the specified options.
        /// </summary>
        /// <param name="options">The configuration options specifying the minimum and maximum bounds for normalization.</param>
        public PercentageNormalizerTransformer(PercentageNormalizerTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentageNormalizerTransformer"/> class with default options.
        /// </summary>
        public PercentageNormalizerTransformer():this(new PercentageNormalizerTransformerOptions())
        {

        }

        /// <summary>
        /// Gets the list of supported numeric source types for percentage normalization.
        /// </summary>
        /// <value>
        /// A list containing all supported integral and floating-point numeric types:
        /// byte, sbyte, ushort, short, uint, int, ulong, long, float, and double.
        /// </value>
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        /// <summary>
        /// Applies percentage normalization to the input data.
        /// </summary>
        /// <param name="inData">The input data to transform.</param>
        /// <param name="outData">
        /// When this method returns, contains the normalized output value as a double in the range [0, 1].
        /// Values equal to Min will produce 0, and values equal to Max will produce 1.
        /// </param>
        /// <returns>Always returns <c>true</c> as the transformation always succeeds.</returns>
        /// <remarks>
        /// The method normalizes the input value by subtracting the minimum and dividing by the range (Max - Min).
        /// Values outside the specified range will produce values outside [0, 1].
        /// </remarks>
        protected override bool OnTransform(DataTransformContext inData, [NotNullWhen(true)] out DataTransformContext? outData)
        {
            var doubleValue = (double)Convert.ChangeType(inData, typeof(double));

            outData = DataTransformContext.From((doubleValue - options.Min) / (options.Max - options.Min), inData.ValueUnit);
            return true;
        }
    }
}
