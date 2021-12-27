using System;

namespace AdventOfCode.Year2021.Day25;

public record SeaCucumber(Vector Direction)
{
	public static SeaCucumber FromChar(char c, Point position)
	{
		switch (c)
		{
			case '>':
				return new SeaCucumber(new Vector(0, 1));
			case 'v':
				return new SeaCucumber(new Vector(1, 0));
			default:
				throw new ArgumentException($"Invalid character: `{c}`");
		}
	}

	public bool IsSoutFacing() => Direction.Row > 0 && Direction.Column == 0;

	public bool IsEastFacing() => Direction.Row == 0 && Direction.Column > 0;
}
