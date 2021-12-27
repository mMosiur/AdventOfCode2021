namespace AdventOfCode.Year2021.Day25;

public readonly record struct Point(int Row, int Column)
{
	public static Point operator +(Point point, Vector vector)
	{
		return new Point(point.Row + vector.Row, point.Column + vector.Column);
	}

	public Point AddModulo(Vector vector, int moduloX, int moduloY)
	{
		return new Point((Row + vector.Row) % moduloX, (Column + vector.Column) % moduloY);
	}
}
