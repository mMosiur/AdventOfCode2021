using System.IO;
using AdventOfCode.Year2021.Day06;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public class Day06Tests
{
	const string BaseDirectory = "Inputs/Day06";

	[Theory]
	[InlineData("exampleInput.txt", "5934")]
	[InlineData("myInput.txt", "387413")]
	public void TestDay06Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day06Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "26984457539")]
	[InlineData("myInput.txt", "1738377086345")]
	public void TestDay06Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day06Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
