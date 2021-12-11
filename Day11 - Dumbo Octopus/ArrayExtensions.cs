namespace AdventOfCode.Year2021.Day11;

public static class ArrayExtensions
{
	public static bool IndexInRange<T>(this T[,] array, int index1, int index2)
	{
		return index1 >= 0 && index1 < array.GetLength(0) && index2 >= 0 && index2 < array.GetLength(1);
	}
}
