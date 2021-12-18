namespace AdventOfCode.Year2021.Day17;

public readonly record struct Position(int X, int Y)
{
	public static readonly Position Origin = new Position(0, 0);

	public static Position operator +(Position position, Velocity velocity)
	{
		return new Position
		{
			X = position.X + velocity.X,
			Y = position.Y + velocity.Y
		};
	}
}
