using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day19;

public class PointRotations : IReadOnlyList<Point>
{
	private readonly Point _point;

	public PointRotations(Point position)
	{
		_point = position;
	}

	public Point this[int index] => index switch
	{
		0 => new(_point.X, _point.Y, _point.Z),
		1 => new(_point.Y, _point.Z, _point.X),
		2 => new(_point.Z, _point.X, _point.Y),
		3 => new(_point.Z, _point.Y, -_point.X),
		4 => new(_point.Y, _point.X, -_point.Z),
		5 => new(_point.X, _point.Z, -_point.Y),
		6 => new(_point.X, -_point.Y, -_point.Z),
		7 => new(_point.Y, -_point.Z, -_point.X),
		8 => new(_point.Z, -_point.X, -_point.Y),
		9 => new(_point.Z, -_point.Y, _point.X),
		10 => new(_point.Y, -_point.X, _point.Z),
		11 => new(_point.X, -_point.Z, _point.Y),
		12 => new(-_point.X, _point.Y, -_point.Z),
		13 => new(-_point.Y, _point.Z, -_point.X),
		14 => new(-_point.Z, _point.X, -_point.Y),
		15 => new(-_point.Z, _point.Y, _point.X),
		16 => new(-_point.Y, _point.X, _point.Z),
		17 => new(-_point.X, _point.Z, _point.Y),
		18 => new(-_point.X, -_point.Y, _point.Z),
		19 => new(-_point.Y, -_point.Z, _point.X),
		20 => new(-_point.Z, -_point.X, _point.Y),
		21 => new(-_point.Z, -_point.Y, -_point.X),
		22 => new(-_point.Y, -_point.X, -_point.Z),
		23 => new(-_point.X, -_point.Z, -_point.Y),
		_ => throw new IndexOutOfRangeException("Invalid rotation index.")
	};

	public int Count => CountOfRotations;

	public static int CountOfRotations => 24;

	public IEnumerator<Point> GetEnumerator()
	{
		for (int i = 0; i < Count; i++)
		{
			yield return this[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
