using System;

namespace AdventOfCode.Year2021.Day02;

public record struct Command(Direction Direction, int Steps)
{
	public static Command Parse(string input)
	{
		string[] parts = input.Trim().Split();
		if (parts.Length != 2)
		{
			throw new FormatException("Invalid input format");
		}
		int steps = int.Parse(parts[1]);
		return parts[0].ToLower() switch
		{
			"forward" => new Command(Direction.Forward, steps),
			"down" => new Command(Direction.Down, steps),
			"up" => new Command(Direction.Up, steps),
			_ => throw new FormatException("Invalid direction")
		};
	}
}
