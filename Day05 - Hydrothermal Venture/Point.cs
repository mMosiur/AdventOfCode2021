using System;

namespace AdventOfCode.Year2021.Day05;

public record struct Point(int X, int Y)
{
	public static Point Parse(string s)
	{
		string[] parts = s.Split(',');
		if (parts.Length != 2)
			throw new FormatException($"{nameof(s)} is not in the correct format");
		try
		{
			return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
		}
		catch (FormatException e)
		{
			throw new FormatException($"{nameof(s)} is not in the correct format", e);
		}
	}

	public override string ToString() => $"({X}, {Y})";
}
