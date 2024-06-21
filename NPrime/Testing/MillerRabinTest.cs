namespace NPrime.Testing
{
    using System;
    using System.Numerics;
    using System.Threading;

    using Utils;

    /// <summary>
    /// Represents the Miller-Rabin primality test.
    /// </summary>
    public class MillerRabinTest : PrimalityTest
    {
        private const int TrialsCountDefault = int.MaxValue >> 16;
        
        private readonly int m_trials;
        private readonly Random m_rng;

        /// <summary>
        /// Creates a new instance of the <see cref="NPrime.Testing.MillerRabinTest"/> class.
        /// </summary>
        public MillerRabinTest() : this(TrialsCountDefault) { }

        /// <summary>
        /// Creates a new instance of the <see cref="NPrime.Testing.MillerRabinTest"/> class
        /// with the specified number of rounds of testing to perform.
        /// </summary>
        /// <param name="trials">A number of rounds of testing to perform.</param>
        public MillerRabinTest(int trials) : base()
        {
            m_rng = new Random(Environment.TickCount);
            m_trials = trials > 0 ? trials : 1;
        }

        private bool MillerTest(int s, ulong d, ulong n, CancellationToken token)
        {
            var a = Convert.ToUInt64(m_rng.NextDouble() * (n - 4)) + 2;
            var x = MathExtensions.ModPow(a, d, n);

            if (x == 1 || x == n - 1)
            {
                return true;
            }

            for (var i = 0; i < s && !token.IsCancellationRequested; ++i)
            {
                x = MathExtensions.ModPow(x, 2, n);
                
                if (x == 1)
                {
                    return false;
                }

                if (x == n - 1)
                {
                    return true;
                }
            }

            return false;
        }

        private bool MillerTest(int s, BigInteger d, BigInteger n, CancellationToken token)
        {
            var temp = n - 1;
            var a = m_rng.NextBigInteger(2, n - 2);
            var x = BigInteger.ModPow(a, d, n);

            if (x == BigInteger.One || x == temp)
            {
                return true;
            }

            for (var i = 0; i < s && !token.IsCancellationRequested; ++i)
            {
                x = BigInteger.ModPow(x, 2, n);
                
                if (x == BigInteger.One)
                {
                    return false;
                }

                if (x == temp)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        protected override PrimalityTestResult InternalTestSmallInteger(ulong n, CancellationToken token)
        {
            if (n <= 3)
            {
                return PrimalityTestResult.Prime;
            }

            if (n % 2 == 0 || n % 3 == 0)
            {
                return PrimalityTestResult.Composite;
            }

            var s = 0;
            var d = n - 1;

            while ((d & 1) != 0 && !token.IsCancellationRequested)
            {
                s++;
                d >>= 1;
            }

            d = (n - 1) / (1uL << s);

            for (var i = 0; i < m_trials && !token.IsCancellationRequested; ++i)
            {
                if (!MillerTest(s, d, n, token))
                {
                    return PrimalityTestResult.Composite;
                }
            }

            return PrimalityTestResult.ProbablyPrime;
        }
        
        /// <inheritdoc />
        protected override PrimalityTestResult InternalTestBigInteger(BigInteger n, CancellationToken token)
        {
            if (n <= 3)
            {
                return PrimalityTestResult.Prime;
            }

            if (n == 4)
            {
                return PrimalityTestResult.Composite;
            }

            var s = 0;
            var d = n - 1;

            while (d.IsEven && !token.IsCancellationRequested)
            {
                s++;
                d >>= 1;
            }

            d = (n - 1) / (1uL << s);
            
            for (var i = 0; i < m_trials && !token.IsCancellationRequested; ++i)
            {
                if (!MillerTest(s, d, n, token))
                {
                    return PrimalityTestResult.Composite;
                }
            }

            return PrimalityTestResult.ProbablyPrime;
        }
    }
}
