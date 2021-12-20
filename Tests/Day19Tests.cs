using System.IO;

using AdventOfCode.Year2021.Day19;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "19")]
public class Day19Tests
{
	const string BaseDirectory = "Inputs/Day19";

	[Theory]
	[InlineData("exampleInput.txt", "79")]
	[InlineData("myInput.txt", "359")]
	public void TestDay19Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day19Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "3621")]
	[InlineData("myInput.txt", "12292")]
	public void TestDay19Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day19Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
