using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Year2021.Day06;

public static class EnumerableExtensions
{
	public static BigInteger Sum(this IEnumerable<BigInteger> source)
	{
		return source.Aggregate((current, value) => current + value);
	}
}
