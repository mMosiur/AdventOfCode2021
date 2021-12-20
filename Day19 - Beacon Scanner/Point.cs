using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021.Day19;

public readonly record struct Point(int X, int Y, int Z)
{
	private static readonly Regex Regex = new Regex(@"^ *(-?\d+) *, *(-?\d+) *, *(-?\d+) *$", RegexOptions.Compiled);

	public static Point Parse(string s)
	{
		Match match = Regex.Match(s);
		if (!match.Success) throw new FormatException($"Invalid position format: \"{s}\".");
		return new()
		{
			X = int.Parse(match.Groups[1].ValueSpan),
			Y = int.Parse(match.Groups[2].ValueSpan),
			Z = int.Parse(match.Groups[3].ValueSpan)
		};
	}

	public static Point operator +(Point point, Vector vector) => new()
	{
		X = point.X + vector.X,
		Y = point.Y + vector.Y,
		Z = point.Z + vector.Z
	};

	public static Vector operator -(Point first, Point second) => new()
	{
		X = first.X - second.X,
		Y = first.Y - second.Y,
		Z = first.Z - second.Z
	};

	public static readonly Point Origin = new(0, 0, 0);

	public int ManhattanDistanceTo(Point point)
	{
		return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z);
	}
}
