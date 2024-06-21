namespace NPrime.Testing
{
    using System;
    using System.Numerics;
    using System.Threading;

    /// <summary>
    /// Represents the trial division test.
    /// </summary>
    public class TrialDivisionTest : PrimalityTest
    {
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

            var s = Convert.ToUInt64(Math.Ceiling(Math.Sqrt(n)));

            for (var d = 5ul; d <= s; d += 6ul)
            {
                if (n % d == 0 || n % (d + 2) == 0)
                {
                    return PrimalityTestResult.Composite;
                }
            }

            return PrimalityTestResult.Prime;
        }
        
        /// <inheritdoc />
        protected override PrimalityTestResult InternalTestBigInteger(BigInteger n, CancellationToken token)
        {
            if (n <= ulong.MaxValue)
            {
                return InternalTestSmallInteger((ulong) n, token);
            }

            if (n.IsEven || n % 3 == BigInteger.Zero)
            {
                return PrimalityTestResult.Composite;
            }

            var d = new BigInteger(5);

            while (BigInteger.Pow(d, 2) <= n)
            {
                if (n % d == 0 || n % (d + 2) == 0)
                {
                    return PrimalityTestResult.Composite;
                }

                d += 6;
            }

            return PrimalityTestResult.Prime;
        }
    }
}