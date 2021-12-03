using System;

using AdventOfCode.Year2021.Day03;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
Day03Solver solver = new Day03Solver(filepath);

Console.Write("Part 1: ");
string part1 = solver.SolvePart1();
Console.WriteLine(part1);

Console.Write("Part 2: ");
string part2 = solver.SolvePart2();
Console.WriteLine(part2);
