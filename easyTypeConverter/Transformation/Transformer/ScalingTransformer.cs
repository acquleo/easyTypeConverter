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
    /// Transforms numeric values by linearly scaling them from an input range to an output range.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The scaling transformer performs linear interpolation to map values from a source range 
    /// (InputMin to InputMax) to a target range (OutputMin to OutputMax).
    /// </para>
    /// <para>
    /// The transformation uses the formula:
    /// <c>output = ((input - InputMin) / (InputMax - InputMin)) * (OutputMax - OutputMin) + OutputMin</c>
    /// </para>
    /// <para>
    /// Common use cases include:
    /// <list type="bullet">
    /// <item><description>Converting sensor readings to engineering units</description></item>
    /// <item><description>Normalizing values to a standard range (e.g., 0-100)</description></item>
    /// <item><description>Mapping analog input ranges to display ranges</description></item>
    /// <item><description>Converting between different measurement scales</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Scale from 0-1023 (10-bit ADC) to 0-100 (percentage)
    /// var options = new ScalingTransformerOptions 
    /// { 
    ///     InputMin = 0, 
    ///     InputMax = 1023,
    ///     OutputMin = 0,
    ///     OutputMax = 100
    /// };
    /// var transformer = new ScalingTransformer(options);
    /// </code>
    /// </example>
    public class ScalingTransformer : DataTransformer
    {
        readonly ScalingTransformerOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingTransformer"/> class with the specified options.
        /// </summary>
        /// <param name="options">The configuration options specifying input and output ranges for scaling.</param>
        public ScalingTransformer (ScalingTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingTransformer"/> class with default options.
        /// </summary>
        public ScalingTransformer ():this(new ScalingTransformerOptions())
        {

        }

        /// <summary>
        /// Gets the list of supported numeric source types for scaling transformation.
        /// </summary>
        /// <value>
        /// A list containing all supported integral and floating-point numeric types:
        /// byte, sbyte, ushort, short, uint, int, ulong, long, float, and double.
        /// </value>
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        /// <summary>
        /// Applies linear scaling transformation to the input data.
        /// </summary>
        /// <param name="inData">The input data to transform.</param>
        /// <param name="outData">
        /// When this method returns, contains the scaled output value as a double.
        /// The value is linearly mapped from the input range to the output range.
        /// </param>
        /// <returns>Always returns <c>true</c> as the transformation always succeeds.</returns>
        /// <remarks>
        /// The method first normalizes the input value to the range [0, 1] based on the input range,
        /// then scales it to the output range. The resulting value is always returned as a double.
        /// </remarks>
        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {

            var doubleValue = (double)Convert.ChangeType(inData.Value, typeof(double));

            var normalized = (doubleValue - options.InputMin) / (options.InputMax - options.InputMin);
            var outValue = normalized * (options.OutputMax - options.OutputMin) + options.OutputMin;
            outData = DataTransformOutput.From(outValue, inData.ValueUnit);
            return true;
        }
    }
}
