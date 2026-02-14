using easyTypeConverter.Transformation.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace easyTypeConverter.Transformation.Transformer
{
    /// <summary>
    /// Implements a deadband filter that only propagates values when the change exceeds a specified threshold.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The deadband transformer filters out minor fluctuations in numeric values by maintaining the last significant value.
    /// A new value is only propagated when the absolute difference from the last value exceeds the configured deadband threshold.
    /// </para>
    /// <para>
    /// The first value always passes through to establish an initial baseline. Subsequent values are compared against
    /// the last propagated value, and only values that differ by more than the deadband are allowed through.
    /// </para>
    /// <para>
    /// This is useful for reducing noise in sensor data, preventing unnecessary updates, and minimizing data storage.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var options = new DeadbandTransformerOptions { Deadband = 5.0 };
    /// var transformer = new DeadbandTransformer(options);
    /// // Values within ±5.0 of the last value will be filtered out
    /// </code>
    /// </example>
    public class DeadbandTransformer : DataTransformer
    {
        DeadbandTransformerOptions options;
        private object? _lastValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeadbandTransformer"/> class with the specified options.
        /// </summary>
        /// <param name="options">The configuration options including the deadband threshold value.</param>
        public DeadbandTransformer(DeadbandTransformerOptions options) : base(options)
        {
            this.options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeadbandTransformer"/> class with default options.
        /// </summary>
        public DeadbandTransformer() : this(new DeadbandTransformerOptions())
        {

        }

        /// <summary>
        /// Gets the list of supported numeric source types for deadband transformation.
        /// </summary>
        /// <value>
        /// A list containing all supported integral and floating-point numeric types:
        /// byte, sbyte, ushort, short, uint, int, ulong, long, float, and double.
        /// </value>
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double) }; }

        /// <summary>
        /// Applies the deadband filter to the input data.
        /// </summary>
        /// <param name="inData">The input data to transform.</param>
        /// <param name="outData">
        /// When this method returns, contains the transformed output data.
        /// If the value change exceeds the deadband, the new value is returned.
        /// Otherwise, the last significant value is returned.
        /// </param>
        /// <returns>Always returns <c>true</c> as the transformation always succeeds.</returns>
        /// <remarks>
        /// The first value received always passes through. Subsequent values are only propagated
        /// if the absolute difference from the last value exceeds the configured deadband threshold.
        /// </remarks>
        protected override bool OnTransform(DataTransformContext inData, [NotNullWhen(true)] out DataTransformContext? outData)
        {
            // First value always passes through
            if (_lastValue is null)
            {
                _lastValue = inData.Value;
                outData = inData;
                return true;
            }

            var delta = ComputeDelta(inData.Value, _lastValue);

            if (delta > options.Deadband)
            {
                _lastValue = inData.Value;
                outData = inData;
                return true;
            }

            outData = DataTransformContext.From(_lastValue, inData.ValueUnit);
            return true;
        }

        /// <summary>
        /// Computes the absolute difference between the current and last values.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="last">The last propagated value.</param>
        /// <returns>The absolute difference as a double-precision floating-point number.</returns>
        /// <remarks>
        /// Both values are converted to double to ensure consistent comparison and avoid overflow during subtraction.
        /// </remarks>
        private double ComputeDelta(object current, object last)
        {
            // Convert both to double to avoid overflow during subtraction
            var currentDouble = Convert.ToDouble(current);
            var lastDouble = Convert.ToDouble(last);

            return Math.Abs(currentDouble - lastDouble);
        }
    }
}
