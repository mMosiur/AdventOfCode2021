using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day08;

public static class EnumerableExtensions
{
	public static IEnumerable<T> Except<T>(this IEnumerable<T> first, T second, params T[] others)
	{
		return first.Except(others.Prepend(second));
	}

	public static IEnumerable<T> Union<T>(this IEnumerable<T> first, T second, params T[] others)
	{
		return first.Union(others.Prepend(second));
	}
}
