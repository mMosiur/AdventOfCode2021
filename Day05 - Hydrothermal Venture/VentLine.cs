using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day05;

public struct VentLine
{
	public Point Start { get; init; }
	public Point End { get; init; }

	public bool IsHorizontal => Start.Y == End.Y;
	public bool IsVertical => Start.X == End.X;

	public IEnumerable<Point> Points
	{
		get
		{
			Point point = Start;
			int dx = End.X - Start.X;
			int dy = End.Y - Start.Y;

			if (Math.Abs(dx) != Math.Abs(dy) && dx != 0 && dy != 0)
			{
				throw new InvalidOperationException("Line is not a straight line");
			}

			int stepX = Math.Sign(dx);
			int stepY = Math.Sign(dy);

			while (true)
			{
				yield return point;
				if (point == End)
					break;
				point = new Point { X = point.X + stepX, Y = point.Y + stepY };
			}
		}
	}

	public VentLine(Point start, Point end)
	{
		Start = start;
		End = end;
	}

	public static VentLine Parse(string s)
	{
		string[] parts = s.Split("->");
		if (parts.Length != 2)
		{
			throw new FormatException($"{nameof(s)} is not in the correct format");
		}
		try
		{
			return new VentLine(Point.Parse(parts[0]), Point.Parse(parts[1]));
		}
		catch (FormatException e)
		{
			throw new FormatException($"{nameof(s)} is not in the correct format", e);
		}
	}

	public override string ToString() => $"[{Start} -> {End}]";
}
