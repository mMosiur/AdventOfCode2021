using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day04;

public static class ArrayExtensions
{
	public static T[,] To2dArray<T>(this IList<IList<T>> jaggedArray)
	{
		if (jaggedArray.Count == 0)
		{
			return new T[0, 0];
		}
		int width = jaggedArray[0].Count;
		if (!jaggedArray.All(row => row.Count == width))
		{
			throw new ArgumentException("All rows must have the same length");
		}
		T[,] result = new T[jaggedArray.Count, width];
		for (int row = 0; row < jaggedArray.Count; row++)
		{
			for (int column = 0; column < width; column++)
			{
				result[row, column] = jaggedArray[row][column];
			}
		}
		return result;
	}
}
