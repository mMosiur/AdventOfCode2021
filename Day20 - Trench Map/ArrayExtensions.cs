using System;
using System.Linq;

namespace AdventOfCode.Year2021.Day20;

public static class ArrayExtensions
{
	public static T GetValueOrDefault<T>(this T[,] array, int row, int col, T defaultValue = default) where T : struct
	{
		if (row < 0 || row >= array.GetLength(0)) return defaultValue;
		if (col < 0 || col >= array.GetLength(1)) return defaultValue;
		return array[row, col];
	}

	public static char[,] ToMatrix(this string[] lines)
	{
		int height = @lines.Length;
		int width = @lines[0].Length;
		if (!lines.All(row => row.Length == width))
			throw new InvalidOperationException("All lines must have the same length.");
		char[,] matrix = new char[height, width];
		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				matrix[row, col] = lines[row][col];
			}
		}
		return matrix;
	}
}
