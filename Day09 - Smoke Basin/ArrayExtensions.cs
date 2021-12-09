namespace AdventOfCode.Year2021.Day09;

public static class ArrayExtensions
{
	public static T GetOrDefault<T>(this T[,] array, int x, int y, T defaultValue) where T : struct
	{
		if (x < 0 || x >= array.GetLength(0) || y < 0 || y >= array.GetLength(1))
		{
			return defaultValue;
		}
		return array[x, y];
	}
}
