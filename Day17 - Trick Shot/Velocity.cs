using System;

namespace AdventOfCode.Year2021.Day17;

public readonly record struct Velocity(int X, int Y)
{
	public Velocity NextStep()
	{
		return new Velocity
		{
			X = X - Math.Sign(X),
			Y = Y - 1
		};
	}
}
