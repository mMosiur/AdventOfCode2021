using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day18;

public static class EnumerableExtensions
{
	public static Pair Sum(this IEnumerable<Pair> pairs)
	{
		return pairs.Aggregate((acc, next) => acc + next);
	}
}
