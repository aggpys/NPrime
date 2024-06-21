namespace NPrime
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    using Properties;

    /// <summary>
    /// Represents the base class for the sieve of prime numbers.
    /// </summary>
    public abstract class PrimeSieve : IPrimeSieve
    {
        protected readonly ConcurrentBag<int> m_primes;
        protected readonly Random m_rng;

        protected readonly int m_limit;
        protected volatile SieveState m_state;

        public virtual int Limit => m_limit;
        public virtual SieveState State => m_state;
        
        /// <inheritdoc />
        public int Count => m_state == SieveState.Initial ? 0 : m_primes.Count;

        protected PrimeSieve(int limit)
        {
            m_limit = limit;
            m_state = SieveState.Initial;

            m_primes = new ConcurrentBag<int>();
            m_rng = new Random();
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginSieve(AsyncCallback callback, object state)
        {
            return BeginSieve(CancellationToken.None, callback, state);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginSieve(CancellationToken token, AsyncCallback callback, object state)
        {
            var asyncResult = Task.Run(() => StartSieving(token), token);
            var completionSource = new TaskCompletionSource<int>(state);

            asyncResult.ContinueWith((task) =>
            {
                if (task.IsFaulted && task.Exception != null)
                {
                    completionSource.TrySetException(task.Exception);                    
                }
                else if (task.IsCanceled)
                {
                    completionSource.TrySetCanceled();
                }
                else
                {
                    completionSource.TrySetResult(task.Result);
                }

                callback?.Invoke(completionSource.Task);
            }, token, TaskContinuationOptions.None, TaskScheduler.Default);

            return completionSource.Task;
        }
        
        /// <inheritdoc />
        public int EndSieve(IAsyncResult asyncResult)
        {
            try
            {
                if (asyncResult is Task<int> task)
                {
                    return task.Result;
                }

                return 0;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
            }

            return 0;
        }
        
        /// <inheritdoc />
        public int Sieve()
        {
            return StartSieving(CancellationToken.None);
        }
        
        /// <inheritdoc />
        public Task<int> SieveAsync(CancellationToken token = default)
        {
            return Task.Factory.StartNew(
                () => StartSieving(token),
                token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginPeekOne(AsyncCallback callback, object state)
        {
            return BeginPeekOne(
                (n) => true, 
                CancellationToken.None, 
                callback, 
                state);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginPeekOne(Func<int, bool> filter, AsyncCallback callback, object state)
        {
            return BeginPeekOne(
                filter, 
                CancellationToken.None,
                callback,
                state);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginPeekOne(Func<int, bool> filter, CancellationToken token, AsyncCallback callback, object state)
        {
            var asyncResult = Task.Run(() => InternalPeekOne(filter, token), token);
            var completionSource = new TaskCompletionSource<int?>(state);

            asyncResult.ContinueWith((task) =>
            {
                if (task.IsFaulted && task.Exception != null)
                {
                    completionSource.TrySetException(task.Exception);
                }
                else if (task.IsCanceled)
                {
                    completionSource.TrySetCanceled();
                }
                else
                {
                    completionSource.TrySetResult(task.Result);
                }

                callback?.Invoke(completionSource.Task);
            }, token, TaskContinuationOptions.None, TaskScheduler.Default);

            return completionSource.Task;
        }
        
        /// <inheritdoc />
        public int? EndPeekOne(IAsyncResult asyncResult)
        {
            try
            {
                if (asyncResult is Task<int?> task)
                {
                    return task.Result;
                }

                return null;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
            }

            return null;
        }
        
        /// <inheritdoc />
        public int? PeekOne()
        {
            return InternalPeekOne((n) => true, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public int? PeekOne(Func<int, bool> filter)
        {
            return InternalPeekOne(filter, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public Task<int?> PeekOneAsync(CancellationToken token = default)
        {
            return Task.Run(() => InternalPeekOne((n) => true, token), token);
        }
        
        /// <inheritdoc />
        public Task<int?> PeekOneAsync(Func<int, bool> filter, CancellationToken token = default)
        {
            return Task.Run(() => InternalPeekOne(filter, token), token);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginSelectAll(AsyncCallback callback, object state)
        {
            return BeginSelectAll((n) => true, Count, callback, state);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginSelectAll(Func<int, bool> filter, AsyncCallback callback, object state)
        {
            return BeginSelectAll(filter, Count, callback, state);
        }

        public IAsyncResult BeginSelectAll(Func<int, bool> filter, int countLimit, AsyncCallback callback, object state)
        {
            return BeginSelectAll(filter, countLimit, CancellationToken.None, callback, state);
        }
        
        /// <inheritdoc />
        public IAsyncResult BeginSelectAll(Func<int, bool> filter, int countLimit, CancellationToken token, AsyncCallback callback, object state)
        {
            var asyncResult = Task.Run(() => InternalSelectAll(filter, countLimit, token), token);
            var completionSource = new TaskCompletionSource<IEnumerable<int>>(state);

            asyncResult.ContinueWith((task) =>
            {
                if (task.IsFaulted && task.Exception != null)
                {
                    completionSource.TrySetException(task.Exception);
                }
                else if (task.IsCanceled)
                {
                    completionSource.TrySetCanceled();
                }
                else
                {
                    completionSource.TrySetResult(task.Result);
                }

                callback?.Invoke(completionSource.Task);
            }, token, TaskContinuationOptions.None, TaskScheduler.Default);

            return completionSource.Task;
        }
        
        /// <inheritdoc />
        public int[] EndSelectAll(IAsyncResult asyncResult)
        {
            try
            {
                if (asyncResult is Task<int[]> task)
                {
                    return task.Result;
                }

                return new int[0];
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
            }

            return new int[0];
        }
        
        /// <inheritdoc />
        public int[] SelectAll()
        {
            return InternalSelectAll((n) => true, Count, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public int[] SelectAll(Func<int, bool> filter)
        {
            return InternalSelectAll(filter, Count, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public int[] SelectAll(Func<int, bool> filter, int countLimit)
        {
            return InternalSelectAll(filter, countLimit, CancellationToken.None);
        }
        
        /// <inheritdoc />
        public Task<int[]> SelectAllAsync(CancellationToken token = default)
        {
            return Task.Run(() => InternalSelectAll((n) => true, Count, token), token);
        }
        
        /// <inheritdoc />
        public Task<int[]> SelectAllAsync(Func<int, bool> filter, CancellationToken token = default)
        {
            return Task.Run(() => InternalSelectAll(filter, Count, token), token);
        }
        
        /// <inheritdoc />
        public Task<int[]> SelectAllAsync(Func<int, bool> filter, int countLimit, CancellationToken token = default)
        {
            return Task.Run(() => InternalSelectAll(filter, countLimit, token), token);
        }

        private int StartSieving(CancellationToken token)
        {
            if (m_state != SieveState.Initial)
            {
                throw new InvalidOperationException(Resources.SieveStateError);
            }

            m_state = SieveState.SievingStarted;

            var count = m_limit < 2 ? 0 : InternalSieve(token);

            m_state = SieveState.SievingCompleted;

            return count;
        }

        /// <summary>
        /// When overriden in a derived class, sieves an integer values to find the prime numbers.
        /// </summary>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>A total amount of the sieved prime numbers.</returns>
        protected abstract int InternalSieve(CancellationToken token);

        /// <summary>
        /// When overriden in a derived class, randomly peeks one of the sieved prime numbers.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>Peeked and filtered prime number, or null.</returns>
        protected virtual int? InternalPeekOne(Func<int, bool> filter, CancellationToken token)
        {
            if (m_state == SieveState.Initial)
            {
                throw new InvalidOperationException(Resources.SieveStateError);
            }

            var temp = InternalSelectAll(filter, Count, token);

            if (temp.Length == 0)
            {
                return null;
            }

            var index = m_rng.Next(0, temp.Length);

            return temp[index];
        }

        /// <summary>
        /// When overriden in a derived class, selects all the sieved prime numbers
        /// using the specified <paramref name="filter"/>. The total amount of the
        /// returned prime numbers will be restricted by the specified count limit.
        /// </summary>
        /// <param name="filter">The method to use to filter the prime numbers.</param>
        /// <param name="countLimit">The limit of count for the output prime numbers.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        /// <returns>An array that contains the selected and filtered prime numbers.</returns>
        protected virtual int[] InternalSelectAll(Func<int, bool> filter, int countLimit, CancellationToken token)
        {
            if (m_state == SieveState.Initial)
            {
                throw new InvalidOperationException(Resources.SieveStateError);
            }

            if (countLimit < 1)
            {
                throw new ArgumentOutOfRangeException(Resources.SieveSelectLimitError, nameof(countLimit));
            }

            var temp = m_primes.ToArray();
            var result = new SortedSet<int>();

            for (var i = 0; i < temp.Length && countLimit > 0 && !token.IsCancellationRequested; ++i)
            {
                if (!filter(temp[i]))
                {
                    continue;
                }
                
                result.Add(temp[i]);
                countLimit--;
            }

            if (result.Count == 0)
            {
                return new int[0];
            }

            var targetArray = new int[result.Count];
            result.CopyTo(targetArray, 0);

            return targetArray;
        }
    }
}
