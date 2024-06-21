namespace NPrime.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    using NPrime;
    using NPrime.Testing;

    public class TrialDivisionTestTests : PrimalityTestTests<TrialDivisionTest> { }
    public class MillerRabinTestTests : PrimalityTestTests<MillerRabinTest> { }

    public abstract class PrimalityTestTests<T> where T : IPrimalityTest
    {
        protected readonly Type m_testType;
        protected readonly Lazy<T> m_defaultInstance;

        protected PrimalityTestTests()
        {
            m_testType = typeof(T);
            m_defaultInstance = new Lazy<T>();
        }

        [Theory]
        [InlineData(10000)]
        public void OnSmallPrimes_ExpectPositiveResult(int limit)
        {
            var instance = m_defaultInstance.Value;
            var sieve = new AtkinSieve(limit);
            sieve.Sieve();

            var primes = sieve.SelectAll();

            Assert.All(primes, (prime) =>
            {
                Assert.Contains(
                    instance.TestInteger(prime), 
                    new[] { PrimalityTestResult.Prime, PrimalityTestResult.ProbablyPrime });
            });
        }

        [Theory]
        [InlineData(100, 2)]
        [InlineData(1000, 3)]
        [InlineData(1000, 5)]
        public void OnSmallNonPrimes_ExpectNegativeResult(int limit, int mul)
        {
            var instance = m_defaultInstance.Value;
            var nonPrimes = Enumerable.Range(2, limit).Select((x) => x * mul);

            Assert.All(nonPrimes, (number) =>
            {
                Assert.True(instance.TestInteger(number) == PrimalityTestResult.Composite);
            });
        }
    }
}
