using System;
using System.IO;
using AdventOfCode.Year2021.Day13;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;

try
{
	var solver = new Day13Solver(filepath);

	Console.Write("Part 1: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.WriteLine("Part 2:");
	string part2 = solver.SolvePart2();
	foreach (char c in part2)
	{
		if (c == (char)PaperSpot.Dot)
		{
			ConsoleColor bgColor = Console.BackgroundColor;
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.Write(c);
			Console.BackgroundColor = bgColor;
		}
		else
		{
			Console.Write(c);
		}
	}
	Console.WriteLine();
}
catch (FileNotFoundException)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"Could not find file `{filepath}`.");
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
