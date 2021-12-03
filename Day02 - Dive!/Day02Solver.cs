using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day02;

public class Day02Solver : DaySolver
{
	private IEnumerable<Command> _commands;

	public Day02Solver(string inputFilePath) : base(inputFilePath)
	{
		_commands = this.InputLines.Select(Command.Parse);
	}

	private int ExecuteCommandsAndGetResult(ISubmarine submarine)
	{
		foreach (Command command in _commands)
		{
			submarine.ExecuteCommand(command);
		}
		return submarine.HorizontalPosition * submarine.Depth;
	}

	public override string SolvePart1() => ExecuteCommandsAndGetResult(new SubmarineWithoutAim()).ToString();

	public override string SolvePart2() => ExecuteCommandsAndGetResult(new SubmarineWithAim()).ToString();
}
