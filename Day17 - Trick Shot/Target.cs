using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021.Day17;

public readonly struct Target
{
	public int MinX { get; }
	public int MaxX { get; }
	public int MinY { get; }
	public int MaxY { get; }

	public Target(int minX, int maxX, int minY, int maxY)
	{
		MinX = minX;
		MaxX = maxX;
		MinY = minY;
		MaxY = maxY;
	}

	public bool ContainsHorizontally(Position position)
	{
		return position.X >= MinX && position.X <= MaxX;
	}

	public bool ContainsVertically(Position position)
	{
		return position.Y >= MinY && position.Y <= MaxY;
	}

	public static Target Parse(string input)
	{
		const string InputPattern = @"^\s*target area:\s*x=(-?\d+)\s?..\s?(-?\d+)\s*,\s*y\s*=\s*(-?\d+)\s?..\s?(-\d+)\s*$";
		Regex regex = new(InputPattern, RegexOptions.IgnoreCase);
		Match match = regex.Match(input);
		int min_x = int.Parse(match.Groups[1].ValueSpan);
		int max_x = int.Parse(match.Groups[2].ValueSpan);
		int min_y = int.Parse(match.Groups[3].ValueSpan);
		int max_y = int.Parse(match.Groups[4].ValueSpan);
		return new Target(min_x, max_x, min_y, max_y);
	}
}
