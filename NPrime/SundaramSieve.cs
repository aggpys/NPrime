namespace NPrime
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    
    /// <summary>
    /// Represents the sieve of Sundaram for finding all prime numbers
    /// up to a specified integer limit.
    /// </summary>
    public class SundaramSieve : PrimeSieve
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NPrime.SundaramSieve"/> 
        /// class with the specified integer limit.
        /// </summary>
        /// <param name="limit">An integer limit of the prime number.</param>
        public SundaramSieve(int limit) : base(limit) { }

        /// <inheritdoc />  
        protected override int InternalSieve(CancellationToken token)
        {
            var sieve = new ConcurrentDictionary<int, bool>();
            var n = (m_limit - 1) / 2;
            var sqrtLimit = Convert.ToInt32(Math.Sqrt(n));
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = token
            };

            Parallel.For(1, sqrtLimit + 1, (i) =>
            {
                var j = i;
                while ((i + j + 2 * i * j <= n) &&
                       (!token.IsCancellationRequested))
                {
                    sieve[i + j + 2 * i * j] = true;
                    j++;
                }
            });

            if (m_limit > 2)
            {
                m_primes.Add(2);
            }

            Parallel.For(1, n + 1, parallelOptions, (i) =>
            {
                if (!sieve.ContainsKey(i))
                {
                    m_primes.Add(2 * i + 1);
                }
            });

            return m_primes.Count;
        }
    }
}
