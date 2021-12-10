using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day10;

/// <summary>
/// With help from https://stackoverflow.com/a/22702269/13783121
/// </summary>
public static class Statistics
{
	/// <summary>
	/// Partitions the given list around a pivot element such that all elements on left of pivot are <= pivot
	/// and the ones at thr right are > pivot. This method can be used for sorting, N-order statistics such as
	/// as median finding algorithms.
	/// Pivot is selected randomly if random number generator is supplied else its selected as last element in the list.
	/// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 171
	/// </summary>
	private static int Partition<T>(this IList<T> list, int start, int end) where T : IComparable<T>
	{
		var pivot = list[end];
		var lastLow = start - 1;
		for (var i = start; i < end; i++)
		{
			if (list[i].CompareTo(pivot) <= 0)
				list.SwapAtIndexes(i, ++lastLow);
		}
		list.SwapAtIndexes(end, ++lastLow);
		return lastLow;
	}

	/// <summary>
	/// Returns Nth smallest element from the list. Here n starts from 0 so that n=0 returns minimum, n=1 returns 2nd smallest element etc.
	/// Note: specified list would be mutated in the process.
	/// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 216
	/// </summary>
	public static double NthOrderStatistic(this IList<double> list, int n)
	{
		return NthOrderStatistic(list, n, 0, list.Count - 1);
	}

	private static double NthOrderStatistic(this IList<double> list, int n, int start, int end)
	{
		while (true)
		{
			var pivotIndex = list.Partition(start, end);
			if (pivotIndex == n)
				return list[pivotIndex];

			if (n < pivotIndex)
				end = pivotIndex - 1;
			else
				start = pivotIndex + 1;
		}
	}

	public static void SwapAtIndexes<T>(this IList<T> list, int i, int j)
	{
		if (i == j)
			return;
		var temp = list[i];
		list[i] = list[j];
		list[j] = temp;
	}

	/// <summary>
	/// Note: specified list would be mutated in the process.
	/// </summary>
	public static double Median(this IList<double> list)
	{
		return list.NthOrderStatistic((list.Count - 1) / 2);
	}

	public static double Median(this IEnumerable<double> sequence)
	{
		List<double> list = new List<double>(sequence);
		int midPoint = (list.Count - 1) / 2;
		return list.NthOrderStatistic(midPoint);
	}
}
