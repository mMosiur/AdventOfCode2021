using System.IO;

using AdventOfCode.Year2021.Day20;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "20")]
public class Day20Tests
{
	const string BaseDirectory = "Inputs/Day20";

	[Theory]
	[InlineData("exampleInput.txt", "35")]
	[InlineData("myInput.txt", "5268")]
	public void TestDay20Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day20Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "3351")]
	[InlineData("myInput.txt", "16875")]
	public void TestDay20Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day20Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
