using System.IO;
using AdventOfCode.Year2021.Day15;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "15")]
public class Day15Tests
{
	const string BaseDirectory = "Inputs/Day15";

	[Theory]
	[InlineData("exampleInput.txt", "40")]
	[InlineData("myInput.txt", "373")]
	public void TestDay15Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day15Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "315")]
	[InlineData("myInput.txt", "2868")]
	public void TestDay15Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day15Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
