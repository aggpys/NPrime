namespace NPrime.Tests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;

    using NPrime;
    using NPrime.Testing;

    public class AtkinSieveTests : PrimeSieveTests<AtkinSieve> { }
    public class EratostheneSieveTests : PrimeSieveTests<EratostheneSieve> { }
    public class SundaramSieveTests : PrimeSieveTests<SundaramSieve> { }

    public abstract class PrimeSieveTests<T> where T : IPrimeSieve
    {
        private readonly Type m_sieveType;

        protected PrimeSieveTests()
        {
            m_sieveType = typeof(T);
        }

        protected IPrimeSieve CreatePrimeSieveInstance(int limit = 0)
        {
            return Activator.CreateInstance(m_sieveType, limit) as IPrimeSieve;
        }

        [Fact]
        public void WithWrongOperationOrder_ExpectFailure()
        {
            var instance = CreatePrimeSieveInstance();

            Assert.Throws<InvalidOperationException>(() => instance.PeekOne());

            instance.Sieve();

            Assert.Throws<InvalidOperationException>(() => instance.Sieve());
        }

        [Theory]
        [InlineData(int.MaxValue / 100)]
        public async Task AfterSieving_ExpectValidSieveState(int limit)
        {
            var instance = CreatePrimeSieveInstance(limit);

            Assert.Equal(SieveState.Initial, instance.State);

            var ar = instance.BeginSieve(null, null);

            await Task.Delay(100);

            Assert.Equal(SieveState.SievingStarted, instance.State);
            
            instance.EndSieve(ar);
            
            Assert.Equal(SieveState.SievingCompleted, instance.State);
        }

        [Theory]
        [InlineData(100000)]
        public void AfterSieving_ExpectUnquePrimes(int limit)
        {
            var instance = CreatePrimeSieveInstance(limit);
            instance.Sieve();

            var primes = instance.SelectAll();

            Assert.Distinct(primes);
        }

        [Theory]
        [InlineData(100, 25)]
        [InlineData(1000, 168)]
        [InlineData(10000, 1229)]
        public void AfterSieving_ExpectAllPrimesFound(int limit, int expectedCount)
        {
            var instance = CreatePrimeSieveInstance(limit);
            var count = instance.Sieve();

            Assert.Equal(expectedCount, count);
            Assert.Equal(expectedCount, instance.Count);
        }


        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void AfterSelect_ExpectAllNumbersPassesPrimalityTest(int limit)
        {
            var instance = CreatePrimeSieveInstance(limit);
            instance.Sieve();

            var primes = instance.SelectAll();

            foreach (var prime in primes)
            {
                Assert.True(PrimalityTest.TrialDivision.TestInteger(prime) == PrimalityTestResult.Prime);
            }
        }

        [Theory]
        [InlineData(100, 1, 5)]
        [InlineData(1000, 3, 42)]
        public void AfterSelectWithCondition_ExpectFilteredPrimes(int limit, int lastDigit, int expectedCount)
        {
            Func<int, bool> condition = (n) => n % 10 == lastDigit;

            var instance = CreatePrimeSieveInstance(limit);
            instance.Sieve();

            var primes = instance.SelectAll(condition);

            Assert.Equal(expectedCount, primes.Length);

            foreach (var prime in primes)
            {
                Assert.True(condition(prime));
            }
        }
    }
}
