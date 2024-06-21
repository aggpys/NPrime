namespace NPrime.Utils
{
    using System.Numerics;

    /// <summary>
    /// Provides static extension methods for <see cref="System.Numerics.BigInteger"/> numbers.
    /// </summary>
    internal static class BigIntegerExtensions
    {
        /// <summary>
        /// Gets the number of bits required for shortest two's complement
        /// representation for the current instance without the sign bit.
        /// </summary>
        public static int GetBitLength(this BigInteger n)
        {
            const int BitsInByteValue = 8;

            if (n < 0)
            {
                n = BigInteger.Abs(n);
            }

            var bits = 0;

            var rawBytes = n.ToByteArray();
                
            if (rawBytes.Length > 1)
            {
                bits = (rawBytes.Length - 1) * BitsInByteValue;
            }

            var lastBit = rawBytes[^1];
            
            while (lastBit != 0)
            {
                lastBit >>= 1;
                bits++;
            }

            return bits;
        }
    }
}