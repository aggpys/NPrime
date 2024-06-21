namespace NPrime.Utils
{
    using System;
    using System.Numerics;
    using Properties;

    /// <summary>
    /// Provides static extension methods for the <see cref="System.Random"/> object.
    /// </summary>
    internal static class RandomExtensions
    {
        /// <summary>
        /// Returns a random <see cref="System.Numerics.BigInteger"/> that is within a specified range.
        /// </summary>
        /// <param name="rng">A pseudo-random number generator.</param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned.</param>
        public static BigInteger NextBigInteger(this Random rng, BigInteger minValue, BigInteger maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(Resources.NextBigIntegerLowerBoundError, nameof(minValue));
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            var d = BigInteger.Abs(maxValue - minValue);
            var len = d.GetBitLength();
            var byteBucket = new byte[len / 8 + 1];

            rng.NextBytes(byteBucket);
            byteBucket[^1] &= (byte) (0xFF >> (8 - len % 8));
            var result = new BigInteger(byteBucket);

            while (result >= d)
            {
                result >>= 1;
            }

            return minValue + result;
        }
    }
}