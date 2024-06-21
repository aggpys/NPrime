namespace NPrime.Utils
{
    using System;
    using Properties;

    /// <summary>
    /// Provides static constants and extension methods for mathematical functions.
    /// </summary>
    internal static class MathExtensions
    {
        /// <summary>
        /// Produces the full product of two unsigned 64-bit numbers.
        /// </summary>
        /// <param name="a">The first unsigned 64-bit number to multiply.</param>
        /// <param name="b">The second unsigned 64-bit number to multiply.</param>
        /// <param name="low">When this method returns, contains the low 64-bit of the product of the specified numbers.</param>
        /// <returns>The high 64-bit of the product of the specified numbers.</returns>
        public static ulong BigMul(ulong a, ulong b, out ulong low)
        {
            var alow = (uint) a;
            var ahigh = (uint) (a >> 32);
            var blow = (uint) b;
            var bhigh = (uint) (b >> 32);

            var mul = ((ulong) alow) * blow;
            var t = ((ulong) ahigh) * blow + (mul >> 32);
            var tlow = ((ulong) alow) * bhigh + (uint) t;

            low = tlow << 32 | (uint) mul;

            return ((ulong) ahigh) + bhigh + (t >> 32) + (tlow >> 32);
        }

        /// <summary>
        /// Performs modulus division on a full product of two unsigned 64-bit numbers.
        /// </summary>
        /// <param name="a">The first unsigned 64-bit number to multiply.</param>
        /// <param name="b">The second unsigned 64-bit number to multiply.</param>
        /// <param name="modulus">The number by which to divide the product of the specified numbers.</param>
        public static ulong BigModMul(ulong a, ulong b, ulong modulus)
        {
            var high = BigMul(a, b, out var low);

            if (high == 0)
            {
                return low % modulus;
            }

            var temp = new decimal(high);
            temp *= 0x100000000;
            temp += low >> 32;
            temp %= modulus;
            temp *= 0x100000000;
            temp += low & 0xFFFFFFFF;

            return (ulong) temp % modulus;
        }

        /// <summary>
        /// Performs modulus division on a number raised to the power of another number.
        /// </summary>
        /// <param name="n">The number to raise to <paramref name="exponent"/> power.</param>
        /// <param name="exponent">The exponent to raise <paramref name="n"/> value by.</param>
        /// <param name="modulus">The number by which to divide number <paramref name="n"/> value raised to <paramref name="exponent"/> power.</param>
        /// <exception cref="System.ArgumentException"/>
        /// <exception cref="System.DivideByZeroException"/>
        public static ulong ModPow(ulong n, ulong exponent, ulong modulus)
        {
            if (modulus == 0)
            {
                throw new DivideByZeroException(Resources.ModPowModulusError);
            }

            if (n <= 1)
            {
                return n;
            }

            if (exponent == 0)
            {
                return modulus == 1 ? 0uL : 1uL;
            }

            var x = n;
            var result = 1uL;

            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = BigModMul(result, x, modulus);
                }

                x = BigModMul(x, x, modulus);
                exponent >>= 1;
            }
                
            return result;
        }
    }
}