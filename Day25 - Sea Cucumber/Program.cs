using System;
using System.IO;
using AdventOfCode.Year2021.Day25;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;

try
{
	var solver = new Day25Solver(filepath);

	Console.Write("Part 1: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);
}
catch (FileNotFoundException)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"Could not find file `{filepath}`.");
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
