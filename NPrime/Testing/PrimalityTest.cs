namespace NPrime.Testing
{
    using System;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the base class for the primality test.
    /// </summary>
    public abstract class PrimalityTest : IPrimalityTest
    {
        private static readonly Dictionary<Type, Lazy<IPrimalityTest>> s_tests;

        /// <summary>
        /// Gets the default instance of the <see cref="NPrime.Testing.TrialDivisionTest"/> class.
        /// </summary>
        public static IPrimalityTest TrialDivision => s_tests[typeof(TrialDivisionTest)].Value;

        /// <summary>
        /// Gets the default instance of the <see cref="NPrime.Testing.MillerRabinTest"/> class.
        /// </summary>
        public static IPrimalityTest MillerRabin => s_tests[typeof(MillerRabinTest)].Value;

        static PrimalityTest()
        {
            s_tests = new Dictionary<Type, Lazy<IPrimalityTest>>()
            {
                { typeof(TrialDivisionTest), new Lazy<IPrimalityTest>(() => new TrialDivisionTest()) },
                { typeof(MillerRabinTest), new Lazy<IPrimalityTest>(() => new MillerRabinTest()) }
            };
        }

        /// <inheritdoc />
        public PrimalityTestResult TestInteger(sbyte n)
        {
            return n <= 1
                ? PrimalityTestResult.Composite
                : InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(byte n)
        {
            return InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(short n)
        {
            return n <= 1
                ? PrimalityTestResult.Composite
                : InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(ushort n)
        {
            return InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(int n)
        {
            return n <= 1
                ? PrimalityTestResult.Composite
                : InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(uint n)
        {
            return InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(long n)
        {
            return n <= 1
                ? PrimalityTestResult.Composite
                : InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(ulong n)
        {
            return InternalTestSmallInteger(Convert.ToUInt64(n), CancellationToken.None);
        }
        
        /// <inheritdoc />
        public PrimalityTestResult TestInteger(BigInteger n)
        {
            return n <= BigInteger.One
                ? PrimalityTestResult.Composite
                : InternalTestBigInteger(n, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(sbyte n, CancellationToken token = default)
        {
            return n <= 1 
                ? Task.FromResult(PrimalityTestResult.Composite) 
                : Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(byte n, CancellationToken token = default)
        {
            return Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(short n, CancellationToken token = default)
        {
            return n <= 1
                ? Task.FromResult(PrimalityTestResult.Composite)
                : Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(ushort n, CancellationToken token = default)
        {
            return Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(int n, CancellationToken token = default)
        {
            return n <= 1
                ? Task.FromResult(PrimalityTestResult.Composite)
                : Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(uint n, CancellationToken token = default)
        {
            return Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(long n, CancellationToken token = default)
        {
            return n <= 1
                ? Task.FromResult(PrimalityTestResult.Composite)
                : Task.Run(() => InternalTestSmallInteger(Convert.ToUInt64(n), token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(ulong n, CancellationToken token = default)
        {
            return Task.Run(() => InternalTestSmallInteger(n, token), token);
        }
        
        /// <inheritdoc />
        public Task<PrimalityTestResult> TestIntegerAsync(BigInteger n, CancellationToken token = default)
        {
            return n <= BigInteger.One
                ? Task.FromResult(PrimalityTestResult.Composite)
                : Task.Run(() => InternalTestBigInteger(n, token), token);
        }

        /// <summary>
        /// When overriden in a derived class, test the specified <see cref="ulong"/>
        /// integer value for primality.
        /// </summary>
        /// <param name="n">An <see cref="ulong"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <seealso cref="PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        protected abstract PrimalityTestResult InternalTestSmallInteger(ulong n, CancellationToken token);
        
        /// <summary>
        /// When overriden in a derived class, test the specified <see cref="System.Numerics.BigInteger"/>
        /// value for primality.
        /// </summary>
        /// <param name="n">A <see cref="System.Numerics.BigInteger"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <seealso cref="PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        protected abstract PrimalityTestResult InternalTestBigInteger(BigInteger n, CancellationToken token);
    }
}
