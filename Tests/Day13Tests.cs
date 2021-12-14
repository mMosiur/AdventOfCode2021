using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Year2021.Day13;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "13")]
public class Day13Tests
{
	const string BaseDirectory = "Inputs/Day13";

	[Theory]
	[InlineData("exampleInput.txt", "17")]
	[InlineData("myInput.txt", "810")]
	public void TestDay13Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day13Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[MemberData(nameof(Part2Data))]
	public void TestDay13Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day13Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}

	public static IEnumerable<string[]> Part2Data =>
		new List<string[]>
		{
			new string[]
			{
				"exampleInput.txt",
@"
#####
#...#
#...#
#...#
#####
.....
.....".Replace("\n", Environment.NewLine).Remove(0, Environment.NewLine.Length)
			},
			new string[]
			{
				"myInput.txt",
@"
#..#.#....###..#..#.###...##..####.###..
#..#.#....#..#.#..#.#..#.#..#.#....#..#.
####.#....###..#..#.###..#....###..#..#.
#..#.#....#..#.#..#.#..#.#.##.#....###..
#..#.#....#..#.#..#.#..#.#..#.#....#.#..
#..#.####.###...##..###...###.#....#..#.".Replace("\n", Environment.NewLine).Remove(0, Environment.NewLine.Length)
			}
		};
}


