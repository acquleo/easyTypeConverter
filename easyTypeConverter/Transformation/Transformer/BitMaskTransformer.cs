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
    /// Extracts one or more bits from an integral value using a bitmask.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The bitmask transformer applies a bitwise AND operation to extract specific bits from integral values.
    /// It supports two modes of operation:
    /// </para>
    /// <para>
    /// <b>Single-bit extraction:</b> When the mask represents a single bit (e.g., 0x01, 0x08, 0x80),
    /// the result is converted to a boolean value (true if the bit is set, false otherwise).
    /// </para>
    /// <para>
    /// <b>Multi-bit extraction:</b> When the mask covers multiple bits (e.g., 0x0F, 0xF0, 0xFF00),
    /// the masked bits can optionally be normalized (right-shifted to the least significant bit position)
    /// to extract the numeric value represented by those bits.
    /// </para>
    /// <para>
    /// Common use cases include:
    /// <list type="bullet">
    /// <item><description>Extracting flag bits from status registers</description></item>
    /// <item><description>Reading specific fields from packed data structures</description></item>
    /// <item><description>Decoding protocol headers or control words</description></item>
    /// <item><description>Extracting bit-packed values from sensor data</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Extract bit 3 (0x08) as a boolean
    /// var singleBitOptions = new BitMaskTransformerOptions { Mask = 0x08 };
    /// var singleBitTransformer = new BitMaskTransformer(singleBitOptions);
    /// // Input: 0x0F → Output: true (bit 3 is set)
    /// 
    /// // Extract bits 4-7 (0xF0) and normalize to 0-15
    /// var multiBitOptions = new BitMaskTransformerOptions { Mask = 0xF0, Normalize = true };
    /// var multiBitTransformer = new BitMaskTransformer(multiBitOptions);
    /// // Input: 0xA5 → Masked: 0xA0 → Normalized: 0x0A (10)
    /// </code>
    /// </example>
    internal class BitMaskTransformer: DataTransformer
    {
        readonly BitMaskTransformerOptions options;
        readonly int _lsbPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMaskTransformer"/> class with the specified options.
        /// </summary>
        /// <param name="options">The configuration options specifying the bitmask and normalization behavior.</param>
        /// <remarks>
        /// The constructor pre-computes the least significant bit (LSB) position of the mask for efficient normalization.
        /// </remarks>
        public BitMaskTransformer(BitMaskTransformerOptions options):base(options)
        {
            this.options = options;
            _lsbPosition = ComputeLsbPosition(options.Mask);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMaskTransformer"/> class with default options.
        /// </summary>
        public BitMaskTransformer():this(new  BitMaskTransformerOptions())
        {

        }

        /// <summary>
        /// Gets the list of supported integral source types for bitmask operations.
        /// </summary>
        /// <value>
        /// A list containing all supported signed and unsigned integral types:
        /// byte, sbyte, ushort, short, uint, int, ulong, and long.
        /// </value>
        /// <remarks>
        /// Floating-point types are not supported as bitwise operations are only meaningful for integral types.
        /// </remarks>
        public override List<Type> SourceTypeList { get => new List<Type>() { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) }; }

        /// <summary>
        /// Applies the bitmask transformation to the input data.
        /// </summary>
        /// <param name="inData">The input data to transform.</param>
        /// <param name="outData">
        /// When this method returns, contains the masked output value.
        /// For single-bit masks, returns a boolean. For multi-bit masks, returns a ulong (normalized if configured).
        /// </param>
        /// <returns>Always returns <c>true</c> as the transformation always succeeds.</returns>
        /// <remarks>
        /// <para>
        /// The transformation follows this logic:
        /// </para>
        /// <list type="number">
        /// <item><description>Apply the mask using bitwise AND</description></item>
        /// <item><description>If the mask is a single bit, return boolean (masked != 0)</description></item>
        /// <item><description>If multi-bit and Normalize is true, right-shift to LSB position</description></item>
        /// <item><description>Otherwise, return the masked value as-is</description></item>
        /// </list>
        /// </remarks>
        protected override bool OnTransform(DataTransformOutput inData, [NotNullWhen(true)] out DataTransformOutput? outData)
        {
            var value = Convert.ToUInt64(inData.Value);
            var masked = value & options.Mask;

            // Single bit → bool
            if (IsSingleBit(options.Mask))
            {
                var val = masked != 0;
                outData =  DataTransformOutput.From(val, inData.ValueUnit);
                return true;
            }

            // Multi bit → shift to LSB if requested
            if (options.Normalize)
            { 
                var val = masked >> _lsbPosition;
                outData = DataTransformOutput.From(val, inData.ValueUnit);
                return true;
            }

            outData = DataTransformOutput.From(masked, inData.ValueUnit);
            return true;
        }

        /// <summary>
        /// Determines whether a mask represents a single bit.
        /// </summary>
        /// <param name="mask">The bitmask to check.</param>
        /// <returns><c>true</c> if the mask has exactly one bit set; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Uses the bit trick: a number is a power of two (single bit) if <c>mask &amp; (mask - 1) == 0</c> and mask is non-zero.
        /// </remarks>
        private static bool IsSingleBit(ulong mask)
            => mask != 0 && (mask & (mask - 1)) == 0;

        /// <summary>
        /// Computes the position of the least significant bit (LSB) in the mask.
        /// </summary>
        /// <param name="mask">The bitmask to analyze.</param>
        /// <returns>The zero-based position of the lowest set bit in the mask.</returns>
        /// <remarks>
        /// This is used to determine how many positions to right-shift when normalizing multi-bit values.
        /// For example, a mask of 0xF0 has an LSB position of 4.
        /// </remarks>
        private static int ComputeLsbPosition(ulong mask)
        {
            // Find position of lowest set bit
            int position = 0;
            while ((mask & 1) == 0)
            {
                mask >>= 1;
                position++;
            }
            return position;
        }
    }
}
