using System;

namespace AdventOfCode.Year2021.Day02;

public class SubmarineWithoutAim : ISubmarine
{
	public int HorizontalPosition { get; private set; }

	public int Depth { get; private set; }

	public void ExecuteCommand(Command command)
	{
		switch (command.Direction)
		{
			case Direction.Forward:
				HorizontalPosition += command.Steps;
				break;
			case Direction.Down:
				Depth += command.Steps;
				break;
			case Direction.Up:
				Depth -= command.Steps;
				break;
			default:
				throw new InvalidOperationException("Invalid direction");
		}
	}
}
