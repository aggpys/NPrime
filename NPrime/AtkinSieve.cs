namespace NPrime
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;

    /// <summary>
    /// Represents the sieve of Atkin for finding all prime numbers
    /// up to a specified integer limit.
    /// </summary>
    public class AtkinSieve : PrimeSieve
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NPrime.AtkinSieve"/> class
        /// with the specified integer limit.
        /// </summary>
        /// <param name="limit">An integer limit of the prime number.</param>
        public AtkinSieve(int limit) : base(limit) { }
        
        /// <inheritdoc />
        protected override int InternalSieve(CancellationToken token)
        {
            var sieve = new ConcurrentDictionary<int, bool>();
            var sqrtLimit = Convert.ToInt32(Math.Sqrt(m_limit));
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = token
            };

            Parallel.For(1, sqrtLimit + 1, parallelOptions, (x) =>
            {
                var qx = x * x;

                Parallel.For(1, sqrtLimit + 1, parallelOptions, (y) =>
                {
                    var qy = y * y;
                    var n = 4 * qx + qy;

                    if (n <= m_limit && (n % 12 == 1 || n % 12 == 5))
                    {
                        sieve.AddOrUpdate(n, true, (index, prev) => !prev);
                    }

                    n = 3 * qx + qy;

                    if (n <= m_limit && n % 12 == 7)
                    {
                        sieve.AddOrUpdate(n, true, (index, prev) => !prev);
                    }

                    if (x <= y)
                    {
                        return;
                    }

                    n = 3 * qx - qy;

                    if (n <= m_limit && n % 12 == 11)
                    {
                        sieve.AddOrUpdate(n, true, (index, prev) => !prev);
                    }
                });
            });

            Parallel.For(5, sqrtLimit + 1, parallelOptions, (r) =>
            {
                if (!sieve.ContainsKey(r) || !sieve[r])
                {
                    return;
                }

                var qr = r * r;

                for (var i = qr; i <= m_limit && !token.IsCancellationRequested; i += qr)
                {
                    sieve.TryUpdate(i, false, true);
                }
            });

            if (m_limit >= 2)
            {
                m_primes.Add(2);
            }

            if (m_limit >= 3)
            {
                m_primes.Add(3);
            }

            Parallel.ForEach(sieve, parallelOptions, (prime) =>
            {
                if (prime.Value)
                {
                    m_primes.Add(prime.Key);
                }
            });

            return m_primes.Count;
        }
    }
}
