using System.IO;
using AdventOfCode.Year2021.Day03;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "03")]
[Trait("Day", "3")]
public class Day03Tests
{
	const string BaseDirectory = "Inputs/Day03";

	[Theory]
	[InlineData("exampleInput.txt", "198")]
	[InlineData("myInput.txt", "1092896")]
	public void TestDay03Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day03Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "230")]
	[InlineData("myInput.txt", "4672151")]
	public void TestDay03Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day03Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
