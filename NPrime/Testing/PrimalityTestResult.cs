namespace NPrime.Testing
{
    /// <summary>
    /// Specifies the primality test results.
    /// </summary>
    public enum PrimalityTestResult
    {
        /// <summary>
        /// The tested number is composite.
        /// </summary>
        Composite,
        /// <summary>
        /// The tested number is exactly prime.
        /// </summary>
        Prime,
        /// <summary>
        /// The tested number is probably prime.
        /// </summary>
        ProbablyPrime
    }
}
