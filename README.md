# NPrime .Net Standard 2.1 library
Provides primality testing tools and the sieves of prime numbers (prime numbers generators)

## Primality testing tools:

### Trial division test

```csharp
var number = BigInteger.Parse("125952159129591959195111295111123");
Console.WriteLine(PrimalityTest.MillerRabin.TestInteger(number));
```

```csharp
public async Task<bool> IsPrime(ulong n)
{
	var trialDivision = new TrialDivisionTest();
	var result = await trialDivision.TestInteger(n);
	
	return result == PrimalityTestResult.Prime;
}
```

### Miller-Rabin test

```csharp
var temp = 1uL;
var millerRabin = new MillerRabinTest(100); // k, or number of trials

while (temp < ulong.MaxValue)
{
	var result = millerRabin.TestInteger(temp);
	
	if (result == PrimalityTestResult.Prime ||
	    result == PrimalityTestResult.ProbablyPrime)
	{
		Console.WriteLine($"{temp}");
	}

	temp += 2;
}
```

## Sieves of the prime numbers:

### Sieve of Atkin

```csharp
var s = new AtkinSieve(10000); // limit

s.Sieve();
int? prime = s.PeekOne((n) => n > 500); // returns random prime > 500
int[] pset = s.SelectAll((n) => n < 100, 10); // returns up to 10 primes less than 100
```

### Sieve of Eratosthenes

```csharp
var s = new EratostheneSieve(10000);

var ar = s.BeginSieve((result) => {
	var count = s.EndSieve(ar);
	Console.WriteLine($"Found {count} prime numbers");
}, s);

```

### Sieve of Sundaram

```csharp
var s = new SundaramSieve(100000000);

await s.SieveAsync(cancellationToken);

await s.SelectAllAsync((n) => n > 1000, 100, cancellationToken)
```


------------
All sieves implements the same interface.
All tests implements the same interface too.
