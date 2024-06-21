namespace NPrime
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the sieve of prime numbers.
    /// </summary>
    public interface IPrimeSieve
    {
        /// <summary>
        /// Gets the sieve limit value.
        /// </summary>
        int Limit { get; }

        /// <summary>
        /// Gets the current amount of the sieved prime numbers.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the current sieve state.
        /// </summary>
        SieveState State { get; }

        /// <summary>
        /// Begins an asynchronous sieve operation.
        /// </summary>
        /// <param name="callback">The method to be called when the asynchronous sieve operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular sieve request from other requests.</param>
        /// <returns>An object that references the asynchronous sieve operation.</returns>
        IAsyncResult BeginSieve(AsyncCallback callback, object state);
        
        /// <summary>
        /// Begins an asynchronous sieve operation.
        /// </summary>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <param name="callback">The method to be called when the asynchronous sieve operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular sieve request from other requests.</param>
        /// <returns>An object that references the asynchronous sieve operation.</returns>
        IAsyncResult BeginSieve(CancellationToken token, AsyncCallback callback, object state);
        
        /// <summary>
        /// Waits for the pending asynchronous sieve operation to complete.
        /// </summary>
        /// <param name="asyncResult">A reference to the pending asynchronous request to wait for.</param>
        /// <returns>A total amount of the sieved prime numbers.</returns>
        int EndSieve(IAsyncResult asyncResult);

        /// <summary>
        /// Sieves an integer values to find the prime numbers.
        /// </summary>
        /// <returns>A total amount of the sieved prime numbers.</returns>
        int Sieve();

        /// <summary>
        /// Asynchronously sieves an integer values to find the prime numbers.
        /// </summary>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous sieve operation and wraps the total
        /// amount of the sieved prime numbers.
        /// </returns>
        Task<int> SieveAsync(CancellationToken token = default);

        /// <summary>
        /// Begins an asynchronous peek operation.
        /// </summary>
        /// <param name="callback">The method to be called when the asynchronous peek operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous peek operation.</returns>
        IAsyncResult BeginPeekOne(AsyncCallback callback, object state);
        
        /// <summary>
        /// Begins an asynchronous peek operation using the specified <paramref name="filter"/> for the prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="callback">The method to be called when the asynchronous peek operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous peek operation.</returns>
        IAsyncResult BeginPeekOne(Func<int, bool> filter, AsyncCallback callback, object state);
        
        /// <summary>
        /// Begins an asynchronous peek operation using the specified <paramref name="filter"/> for the prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <param name="callback">The method to be called when the asynchronous peek operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous peek operation.</returns>
        IAsyncResult BeginPeekOne(Func<int, bool> filter, CancellationToken token, AsyncCallback callback, object state);
        
        /// <summary>
        /// Waits for the pending asynchronous peek operation to complete.
        /// </summary>
        /// <param name="asyncResult">A reference to the pending asynchronous request to wait for.</param>
        /// <returns>Peeked prime number, or null.</returns>
        int? EndPeekOne(IAsyncResult asyncResult);

        /// <summary>
        /// Randomly peeks one of the sieved prime numbers.
        /// </summary>
        /// <returns>Peeked prime number, or null.</returns>
        int? PeekOne();

        /// <summary>
        /// Randomly peeks one of the sieved prime numbers using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <returns>Peeked and filtered prime number, or null.</returns>
        int? PeekOne(Func<int, bool> filter);

        /// <summary>
        /// Asynchronously peeks the random one of the sieved prime numbers.
        /// </summary>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous peek operation and wraps the peeked prime number value.
        /// </returns>
        Task<int?> PeekOneAsync(CancellationToken token = default);

        /// <summary>
        /// Asynchronously peeks the random one of the sieved prime numbers
        /// using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous peek operation and wraps the peeked prime number value.
        /// </returns>
        Task<int?> PeekOneAsync(Func<int, bool> filter, CancellationToken token = default);

        /// <summary>
        /// Begins an asynchronous select operation.
        /// </summary>
        /// <param name="callback">The method to be called when the asynchronous select operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous select operation.</returns>
        IAsyncResult BeginSelectAll(AsyncCallback callback, object state);

        /// <summary>
        /// Begins an asynchronous select operation using the specified <paramref name="filter"/> for the prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="callback">The method to be called when the asynchronous select operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous select operation.</returns>
        IAsyncResult BeginSelectAll(Func<int, bool> filter, AsyncCallback callback, object state);
        
        /// <summary>
        /// Begins an asynchronous select operation with the limited output count
        /// using the specified <paramref name="filter"/> for the prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="countLimit">The count limit for the output prime numbers.</param>
        /// <param name="callback">The method to be called when the asynchronous select operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous select operation.</returns>
        IAsyncResult BeginSelectAll(Func<int, bool> filter, int countLimit, AsyncCallback callback, object state);
        
        /// <summary>
        /// Begins an asynchronous select operation with the limited output count
        /// using the specified <paramref name="filter"/> for the prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="countLimit">The limit of count for the output prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <param name="callback">The method to be called when the asynchronous select operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular peek request from other requests.</param>
        /// <returns>An object that references the asynchronous select operation.</returns>
        IAsyncResult BeginSelectAll(Func<int, bool> filter, int countLimit, CancellationToken token, AsyncCallback callback, object state);
        
        /// <summary>
        /// Waits for the pending asynchronous select operation to complete.
        /// </summary>
        /// <param name="asyncResult">A reference to the pending asynchronous request to wait for.</param>
        /// <returns>An array that contains the selected prime numbers.</returns>
        int[] EndSelectAll(IAsyncResult asyncResult);

        /// <summary>
        /// Selects all the sieved prime numbers.
        /// </summary>
        /// <returns>An array that contains the selected prime numbers.</returns>
        int[] SelectAll();

        /// <summary>
        /// Selects all the sieved prime numbers using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <returns>An array that contains the selected and filtered prime numbers.</returns>
        int[] SelectAll(Func<int, bool> filter);

        /// <summary>
        /// Selects all the sieved prime numbers using the specified <paramref name="filter"/>.
        /// The total amount of the returned prime numbers will be restricted
        /// by the specified count limit.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="countLimit">The limit of count for the output prime numbers.</param>
        /// <returns>An array that contains the selected and filtered prime numbers.</returns>
        int[] SelectAll(Func<int, bool> filter, int countLimit);

        /// <summary>
        /// Asynchronously selects all the sieved prime numbers.
        /// </summary>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous select operation and wraps an array of selected prime numbers.
        /// </returns>
        Task<int[]> SelectAllAsync(CancellationToken token = default);

        /// <summary>
        /// Asynchronously selects all the sieved prime numbers using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous select operation and wraps an array of selected prime numbers.
        /// </returns>
        Task<int[]> SelectAllAsync(Func<int, bool> filter, CancellationToken token = default);

        /// <summary>
        /// Asynchronously selects all the sieved prime numbers using the specified <paramref name="filter"/>.
        /// The total amount of the returned prime numbers will be restricted by the specified count limit.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="countLimit">The limit of count for the output prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous select operation and wraps an array of selected prime numbers.
        /// </returns>
        Task<int[]> SelectAllAsync(Func<int, bool> filter, int countLimit, CancellationToken token = default);
    }
}
