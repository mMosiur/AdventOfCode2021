using System;

namespace AdventOfCode.Year2021.Day02;

public class SubmarineWithAim : ISubmarine
{
	public int HorizontalPosition { get; private set; }

	public int Depth { get; private set; }

	public int Aim { get; private set; }

	public void ExecuteCommand(Command command)
	{
		switch (command.Direction)
		{
			case Direction.Forward:
				HorizontalPosition += command.Steps;
				Depth += Aim * command.Steps;
				break;
			case Direction.Down:
				Aim += command.Steps;
				break;
			case Direction.Up:
				Aim -= command.Steps;
				break;
			default:
				throw new InvalidOperationException("Invalid direction");
		}
	}
}
