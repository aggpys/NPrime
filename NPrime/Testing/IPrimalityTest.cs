namespace NPrime.Testing
{
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the primality test for the numbers.
    /// </summary>
    public interface IPrimalityTest
    {
        /// <summary>
        /// Tests the specified <see cref="sbyte"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="sbyte"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(sbyte n);
        
        /// <summary>
        /// Tests the specified <see cref="byte"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="byte"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(byte n);
        
        /// <summary>
        /// Tests the specified <see cref="short"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="short"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(short n);
        
        /// <summary>
        /// Tests the specified <see cref="ushort"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="ushort"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(ushort n);
        
        /// <summary>
        /// Tests the specified <see cref="int"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="int"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(int n);
        
        /// <summary>
        /// Tests the specified <see cref="uint"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="uint"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(uint n);
        
        /// <summary>
        /// Tests the specified <see cref="long"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="long"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(long n);
        
        /// <summary>
        /// Tests the specified <see cref="ulong"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="ulong"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(ulong n);
        
        /// <summary>
        /// Tests the specified <see cref="System.Numerics.BigInteger"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="System.Numerics.BigInteger"/> value to test for.</param>
        /// <returns>
        /// A <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        PrimalityTestResult TestInteger(BigInteger n);

        /// <summary>
        /// Asynchronously tests the specified <see cref="sbyte"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="sbyte"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(sbyte n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="byte"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="byte"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(byte n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="short"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="short"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(short n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="ushort"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="ushort"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(ushort n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="int"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="int"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(int n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="uint"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="uint"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(uint n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="long"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="long"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(long n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="ulong"/> number for primality.
        /// </summary>
        /// <param name="n">An <see cref="ulong"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(ulong n, CancellationToken token = default);
        
        /// <summary>
        /// Asynchronously tests the specified <see cref="System.Numerics.BigInteger"/> number for primality.
        /// </summary>
        /// <param name="n">A <see cref="System.Numerics.BigInteger"/> value to test for.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous test operation and wraps
        /// the <seealso cref="NPrime.Testing.PrimalityTestResult"/> value that indicates
        /// whether the number specified was prime, probably prime,
        /// or composite.
        /// </returns>
        Task<PrimalityTestResult> TestIntegerAsync(BigInteger n, CancellationToken token = default);
    }
}
