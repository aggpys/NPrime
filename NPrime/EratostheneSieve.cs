namespace NPrime
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;

    /// <summary>
    /// Represents the sieve of Eratosthenes for finding all prime numbers
    /// up to a specified integer limit.
    /// </summary>
    public class EratostheneSieve : PrimeSieve
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NPrime.EratostheneSieve"/> class
        /// with the specified integer limit.
        /// </summary>
        /// <param name="limit">An integer limit of the prime number.</param>
        public EratostheneSieve(int limit) : base(limit) { }

        /// <inheritdoc />
        protected override int InternalSieve(CancellationToken token)
        {
            var sieve = new ConcurrentDictionary<int, bool>();
            var sqrtLimit = Convert.ToInt32(Math.Sqrt(m_limit));
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = token
            };

            Parallel.For(2, sqrtLimit + 1, parallelOptions, (i) =>
            {
                for (var j = i * i; j <= m_limit; j += i)
                {
                    sieve[j] = true;
                }
            });

            Parallel.For(2, m_limit, parallelOptions, (i) =>
            {
                if (!sieve.ContainsKey(i))
                {
                    m_primes.Add(i);
                }
            });

            return m_primes.Count;
        }
    }
}
